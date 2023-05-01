using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;

namespace ECommerce.Infrastructure.Services;

internal class ProductService : IProductService
{
    public Task<IEnumerable<Product>> GetAllCategories()
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetCategoryById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategory(Guid id)
    {
        throw new NotImplementedException();
    }

}
