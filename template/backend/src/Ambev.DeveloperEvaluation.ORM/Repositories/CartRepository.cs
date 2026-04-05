using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;
        private readonly IDistributedCache _cache;
        private const string CachePrefix = "Cart:";
        private readonly JsonSerializerOptions _jsonOptions;

        public CartRepository(DefaultContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return cart;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cart = await GetByIdAsync(id, cancellationToken);
            if (cart == null)
                return false;

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);

            // Invalidate cache
            await _cache.RemoveAsync($"{CachePrefix}{id}", cancellationToken);

            return true;
        }

        public IQueryable<Cart> GetAll(CancellationToken cancellationToken = default)
        {
            return _context.Carts
                .Include(x => x.Products)
                .Include(x => x.User)
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"{CachePrefix}{id}";
            var cachedCart = await _cache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedCart))
            {
                return JsonSerializer.Deserialize<Cart>(cachedCart, _jsonOptions);
            }

            var cart = await _context.Carts
                    .Include(x => x.Products)
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (cart != null)
            {
                await _cache.SetStringAsync(
                    cacheKey, 
                    JsonSerializer.Serialize(cart, _jsonOptions), 
                    new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(10) },
                    cancellationToken);
            }

            return cart;
        }

        public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            var existingItems = await _context.Set<CartProducts>()
                .Where(cp => cp.CartId == cart.Id)
                .ToListAsync(cancellationToken);

            _context.Set<CartProducts>().RemoveRange(existingItems);
            
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync(cancellationToken);

            // Invalidate cache
            await _cache.RemoveAsync($"{CachePrefix}{cart.Id}", cancellationToken);
        }
    }
}
