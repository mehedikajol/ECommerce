using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;

namespace ECommerce.Infrastructure.Repositories;

internal class CategoryRepository : GenericRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) 
        : base(context)
    {
    }
}
