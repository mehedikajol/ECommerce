using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategories(); 
    Task<Category> GetCategoryById(Guid id);
    Task CreateCategory(Category category);
    Task UpdateCategory(Category category);
    Task DeleteCategory(Guid id);
}
