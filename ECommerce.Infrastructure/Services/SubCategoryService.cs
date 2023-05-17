using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Exceptions;
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
                CategoryName = entity.Category.Name,
                MainCategoryName = entity.Category.MainCategory.ToString(),
            });
        }

        return subCategories;
    }

    public async Task<SubCategory> GetSubCategoryById(Guid id)
    {
        var subCategoryEntity = await _unitOfWork.SubCategories.GetEntityById(id);
        if (subCategoryEntity is null) 
                throw new NotFoundException("SUbCategory not found.");

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
        var alreadyUsedName = await IsNameAlreadyUsed(subCategory.Name);
        if (alreadyUsedName)
            throw new DuplicatePropertyException("SubCategory name is already in use.");

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
        var alreadyUsedName = await IsNameAlreadyUsed(category.Name, category.Id);
        if (alreadyUsedName)
            throw new DuplicatePropertyException("Category name is already in use.");

        var subCategoryEntity = await _unitOfWork.SubCategories.GetEntityById(category.Id);
        if (subCategoryEntity is null)
            throw new NotFoundException("SubCategory not found.");

        subCategoryEntity.Name = category.Name;
        subCategoryEntity.Description = category.Description;
        subCategoryEntity.CategoryId = category.CategoryId;

        await _unitOfWork.SubCategories.UpdateEntity(subCategoryEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteSubCategory(Guid id)
    {
        var subCategoryEntity = await _unitOfWork.SubCategories.GetEntityById(id);
        if (subCategoryEntity is null)
            throw new NotFoundException("SubCategory not found.");

        await _unitOfWork.SubCategories.DeleteEntityById(id);
        await _unitOfWork.CompleteAsync();
    }

    private async Task<bool> IsNameAlreadyUsed(string name) =>
         await _unitOfWork.SubCategories.GetCount(sc => sc.Name == name.Trim()) > 0;

    private async Task<bool> IsNameAlreadyUsed(string name, Guid subCategoryId) =>
        await _unitOfWork.SubCategories.GetCount(sc => sc.Name == name.Trim() && sc.Id != subCategoryId) > 0;
}
