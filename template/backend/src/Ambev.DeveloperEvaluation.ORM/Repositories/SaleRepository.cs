using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return sale;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await GetByIdAsync(id, cancellationToken);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public IQueryable<Sale> GetAll(CancellationToken cancellationToken = default)
        {
            return _context.Sales
                .Include(x => x.Products)
                .Include(x => x.User)
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task UpdateSaleAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales
                .Where(s => s.Id == sale.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.Products, sale.Products)
                    .SetProperty(s => s.Status, sale.Status)
                    .SetProperty(s => s.Quantity, sale.Quantity)
                    .SetProperty(s => s.Branch, sale.Branch)
                    .SetProperty(s => s.TotalSaleAmount, sale.TotalSaleAmount),
                    cancellationToken);
        }
    }
}
