using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

internal class SubCategoryRepository : GenericRepository<SubCategory, Guid>, ISubCategoryRepository
{
    public SubCategoryRepository(AppDbContext context)
        : base(context)
    {
    }

    public override async Task<IEnumerable<SubCategory>> GetAllEntities()
    {
        var entities = await _dbSet.Include(sc => sc.Category).ToListAsync();
        return entities;
    }
}
