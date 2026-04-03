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
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await _context.Carts
                .Where(s => s.Id == cart.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.Products, cart.Products)
                    .SetProperty(s => s.Status, cart.Status)
                    .SetProperty(s => s.Quantity, cart.Quantity)
                    .SetProperty(s => s.Branch, cart.Branch)
                    .SetProperty(s => s.TotalCartAmount, cart.TotalCartAmount),
                    cancellationToken);
        }
    }
}
