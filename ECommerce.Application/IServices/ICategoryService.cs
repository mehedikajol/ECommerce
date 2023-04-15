

using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task CreateCategory(Category category);
    }
}
