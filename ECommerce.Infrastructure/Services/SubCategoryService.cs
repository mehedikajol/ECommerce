using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
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

    public async Task<SubCategory> GetSubCategoryById(Guid id)
    {
        var subCategoryEntity = await _unitOfWork.SubCategories.GetEntityById(id);
        if (subCategoryEntity is null) return null;
        var subCategory = new SubCategory
        {
            Id = subCategoryEntity.Id,
            Name = subCategoryEntity.Name,
            Description = subCategoryEntity.Description,
            CategoryId = subCategoryEntity.CategoryId
        };
        return subCategory;
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

    public async Task UpdateSubCategory(SubCategory category)
    {
        var subCategoryEntity = await _unitOfWork.SubCategories.GetEntityById(category.Id);
        subCategoryEntity.Name = category.Name;
        subCategoryEntity.Description = category.Description;
        subCategoryEntity.CategoryId = category.CategoryId;

        await _unitOfWork.SubCategories.UpdateEntity(subCategoryEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteSubCategory(Guid id)
    {
        await _unitOfWork.SubCategories.DeleteEntityById(id);
        await _unitOfWork.CompleteAsync();
    }

   
}
