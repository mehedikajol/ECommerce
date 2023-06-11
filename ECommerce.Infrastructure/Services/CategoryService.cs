using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Enums;
using ECommerce.Core.Exceptions;
using Mapster;
using EO = ECommerce.Core.Entities;

namespace ECommerce.Infrastructure.Services;

internal class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        var categories = new List<Category>();
        var categoryEntities = await _unitOfWork.Categories.GetAllEntities();

        foreach (var entity in categoryEntities)
        {
            var category = entity.Adapt<Category>();
            category.MainCategory = (int)entity.MainCategory;
            category.MainCategoryName = entity.MainCategory.ToString();

            categories.Add(category);
        }
        return categories;
    }

    public async Task<Category> GetCategoryById(Guid id)
    {
        var categoryEntity = await _unitOfWork.Categories.GetEntityById(id);
        if (categoryEntity is null) 
            throw new NotFoundException("Category not found.");


        var category = categoryEntity.Adapt<Category>();
        category.MainCategory = (int)categoryEntity.MainCategory;

        return category;
    }

    public async Task CreateCategory(Category category)
    {
        var alreadyUsedName = await IsNameAlreadyUsed(category.Name);
        if (alreadyUsedName)
            throw new DuplicatePropertyException("Category name is already in use.");

        var categoryEntity = new EO.Category
        {
            Name = category.Name.Trim(),
            Description = category.Description,
            MainCategory = (MainCategory)category.MainCategory
        };

        await _unitOfWork.Categories.AddEntity(categoryEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateCategory(Category category)
    {
        var alreadyUsedName = await IsNameAlreadyUsed(category.Name, category.Id);
        if(alreadyUsedName)
            throw new DuplicatePropertyException("Category name is already in use.");

        var categoryEntity = await _unitOfWork.Categories.GetEntityById(category.Id);
        if (categoryEntity is null)
            throw new NotFoundException("Category not found.");

        categoryEntity.Name = category.Name.Trim();
        categoryEntity.Description = category.Description;
        categoryEntity.MainCategory = (MainCategory)category.MainCategory;

        await _unitOfWork.Categories.UpdateEntity(categoryEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteCategory(Guid id)
    {
        var categoryEntity = await _unitOfWork.Categories.GetEntityById(id);
        if (categoryEntity is null)
            throw new NotFoundException("Category not found.");

        await _unitOfWork.Categories.DeleteEntityById(id);
        await _unitOfWork.CompleteAsync();
    }

    private async Task<bool> IsNameAlreadyUsed(string name) =>
        await _unitOfWork.Categories.GetCount(c => c.Name == name.Trim()) > 0;

    private async Task<bool> IsNameAlreadyUsed(string name, Guid categoryId) =>
        await _unitOfWork.Categories.GetCount(c => c.Name == name.Trim() && c.Id != categoryId) > 0;
}
