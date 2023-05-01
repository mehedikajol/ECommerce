using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Enums;
using EO = ECommerce.Core.Entities;

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

    public async Task CreateSubCategory(SubCategory subCategory)
    {
        var subCategoryEntity = new EO.SubCategory
        {
            Name = subCategory.Name,
            Description = subCategory.Description,
            CategoryId = subCategory.CategoryId
        };

        await _unitOfWork.SubCategories.AddEntity(subCategoryEntity);
        await _unitOfWork.CompleteAsync();
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
