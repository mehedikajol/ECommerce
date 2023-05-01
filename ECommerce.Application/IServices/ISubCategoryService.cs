using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices;

public interface ISubCategoryService
{
    Task<IEnumerable<SubCategory>> GetAllSubCategories();
    Task<SubCategory> GetSubCategoryById(Guid id);
    Task CreateSubCategory(SubCategory category);
    Task UpdateSubCategory(SubCategory category);
    Task DeleteSubCategory(Guid id);
}
