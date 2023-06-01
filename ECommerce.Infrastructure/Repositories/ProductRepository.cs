using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Core.Enums;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System.Resources;

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

    public async Task<IEnumerable<Product>> GetFilteredProductsAsync(string searchTerm = null, int sortValue = 0, int pageSize = 12)
    {
        var query = _dbSet.AsQueryable();

        // Filter by sort value
        query = (ProductSortValue)sortValue switch
        {
            ProductSortValue.Name => query.OrderBy(p => p.Name),
            ProductSortValue.Popularity => query,
            ProductSortValue.Newest => query.OrderByDescending(p => p.InsertedDate),
            ProductSortValue.PriceLowToHigh => query.OrderBy(p => (int)p.Price),
            ProductSortValue.PriceHighToLow => query.OrderByDescending(p => (int)p.Price),
            _ => query
        };

        // Filter by search term
        if (!string.IsNullOrWhiteSpace(searchTerm))
            query = query.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
        

        return await query.Include(p => p.SubCategory).Take(pageSize).ToListAsync();
    }
}
