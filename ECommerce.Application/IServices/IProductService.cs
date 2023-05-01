using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllCategories();
    Task<Category> GetCategoryById(Guid id);
    Task CreateCategory(Category category);
    Task UpdateCategory(Category category);
    Task DeleteCategory(Guid id);
}
