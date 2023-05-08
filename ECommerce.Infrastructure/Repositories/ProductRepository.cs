using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

internal class ProductRepository : GenericRepository<Product, Guid>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Product>> GetAllEntities()
    {
        return await _dbSet.Include(p => p.SubCategory).ToListAsync();
    }

    public override async Task<Product> GetEntityById(Guid id)
    {
        return await _dbSet.Include(p => p.SubCategory).FirstOrDefaultAsync(p => p.Id == id);
    }
}
