using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Enums;
using ECommerce.Core.Exceptions;
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
            categories.Add(new Category
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                MainCategory = (int)entity.MainCategory,
                MainCategoryName = Enum.GetName(typeof(MainCategory), entity.MainCategory)
                //MainCategory = Enum.GetName(typeof(MainCategory), entity.MainCategoryId)
            });
        }
        return categories;
    }

    public async Task<Category> GetCategoryById(Guid id)
    {
        var categoryEntity = await _unitOfWork.Categories.GetEntityById(id);
        if (categoryEntity is null) 
            throw new NotFoundException("Category not found.");

        var category = new Category
        {
            Id = categoryEntity.Id,
            Name = categoryEntity.Name,
            Description = categoryEntity.Description,
            MainCategory = (int)categoryEntity.MainCategory
        };
        return category;
    }

    public async Task CreateCategory(Category category)
    {
        var alreadyUsedName = await IsNameAlreadyUsed(category.Name);
        if (alreadyUsedName)
            throw new DuplicateNameException("Category name is already in use.");

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
            throw new DuplicateNameException("Category name is already in use.");

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
