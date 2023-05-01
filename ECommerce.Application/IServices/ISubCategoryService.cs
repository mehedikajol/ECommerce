using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices;

public interface ISubCategoryService
{
    Task<IEnumerable<SubCategory>> GetAllSubCategories();
    Task CreateSubCategory(SubCategory category);
    Task<SubCategory> GetSubCategoryById(Guid id);
    Task UpdateSubCategory(SubCategory category);
    Task DeleteSubCategory(Guid id);
}
