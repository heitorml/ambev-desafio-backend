using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        public CartRepository(DefaultContext context)
        {
            _context = context;
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
            return await _context.Carts
                    .Include(x => x.Products)
                    .Include(x => x.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            var existingItems = await _context.Set<CartProducts>()
                .Where(cp => cp.CartId == cart.Id)
                .ToListAsync(cancellationToken);

            _context.Set<CartProducts>().RemoveRange(existingItems);
            
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
