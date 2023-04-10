using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context, ILogger logger) 
            : base(context, logger)
        {
        }
    }
}
