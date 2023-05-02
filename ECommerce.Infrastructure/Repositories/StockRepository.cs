using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ECommerce.Infrastructure.Repositories;

internal class StockRepository : GenericRepository<Stock, Guid>, IStockRepository
{
    public StockRepository(AppDbContext context) 
        : base(context)
    {
    }

    public override async Task<IEnumerable<Stock>> GetAllEntities()
    {
        return await _dbSet
            .Include(s => s.Product)
                .ThenInclude(p => p.SubCategory)
            .ToListAsync();
    }

    public override async Task<Stock> GetEntityById(Guid id)
    {
        return await _dbSet
            .Include(s => s.Product)
                .ThenInclude(p => p.SubCategory)
            .Where(s => s.Id == id).FirstOrDefaultAsync();
    }
}
