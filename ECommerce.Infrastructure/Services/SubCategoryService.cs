using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;

namespace ECommerce.Infrastructure.Services;

internal class SubCategoryService : ISubCategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public SubCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SubCategory>> GetAllSubCategories()
    {
        var subCategories = new List<SubCategory>();
        var subCategoryEntities = await _unitOfWork.SubCategories.GetAllEntities();

        foreach (var entity in subCategoryEntities)
        {
            subCategories.Add(new SubCategory
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CategoryId = entity.CategoryId,
                CategoryName = entity.Category.Name
            });
        }

        return subCategories;
    }

    public Task<SubCategory> GetSubCategoryById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateSubCategory(SubCategory category)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSubCategory(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateSubCategory(SubCategory category)
    {
        throw new NotImplementedException();
    }
}
