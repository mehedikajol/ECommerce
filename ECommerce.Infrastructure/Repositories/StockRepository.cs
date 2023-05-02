using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;

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
}
