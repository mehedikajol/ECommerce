using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;

namespace ECommerce.Infrastructure.Repositories;

internal class SubCategoryRepository : GenericRepository<SubCategory, Guid>, ISubCategoryRepository
{
    public SubCategoryRepository(AppDbContext context)
        : base(context)
    {
    }
}
