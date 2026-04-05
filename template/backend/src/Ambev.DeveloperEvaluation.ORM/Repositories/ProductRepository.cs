using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;
        private readonly IDistributedCache _cache;
        private const string CachePrefix = "Product:";

        public ProductRepository(DefaultContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await GetByIdAsync(id, cancellationToken);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);

            // Invalidate cache
            await _cache.RemoveAsync($"{CachePrefix}{id}", cancellationToken);

            return true;
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);

            // Invalidate cache
            await _cache.RemoveAsync($"{CachePrefix}{product.Id}", cancellationToken);
        }

        public IQueryable<Product?> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _context.Products.AsNoTracking().AsQueryable();
        }


        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"{CachePrefix}{id}";
            var cachedProduct = await _cache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedProduct))
            {
                return JsonSerializer.Deserialize<Product>(cachedProduct);
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (product != null)
            {
                await _cache.SetStringAsync(
                    cacheKey, 
                    JsonSerializer.Serialize(product), 
                    new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(10) },
                    cancellationToken);
            }

            return product;
        }

        public async Task<List<Product>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Where(p => ids.Contains(p.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
