using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Areas.Admin.Models.SubCategories;

public class SubCategoryEditModel
{
    private ISubCategoryService _subCategoryService;
    private ICategoryService _categoryService;
    private ILifetimeScope _scope;

    public SubCategoryEditModel()
    {
    }

    public SubCategoryEditModel(
        ISubCategoryService subCategoryService,
        ICategoryService categoryService)
    {
        _subCategoryService = subCategoryService;
        _categoryService = categoryService;

    }

    internal void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _subCategoryService = _scope.Resolve<ISubCategoryService>();
        _categoryService = _scope.Resolve<ICategoryService>();
    }
    public Guid Id { get; set; }
    [Required, MaxLength(50, ErrorMessage = "Name can't be more than 50 characters!")]
    public string Name { get; set; }

    [MaxLength(250, ErrorMessage = "Description can't be more than 250 characters!")]
    public string Description { get; set; }

    [Required]
    public Guid Category { get; set; }

    public async Task<IEnumerable<Category>> LoadCategories()
    {
        return await _categoryService.GetAllCategories();
    }

    public bool IsValidItem { get; set; }

    internal async Task LoadData(Guid id)
    {
        var category = await _subCategoryService.GetSubCategoryById(id);
        if (category is not null)
        {
            Name = category.Name;
            Description = category.Description;
            Category = category.CategoryId;
            IsValidItem = true;
        }
        else
        {
            IsValidItem = false;
        }
    }

    internal async Task UpdateSubCategory()
    {
        var subCategory = new SubCategory
        {
            Id = Id,
            Name = Name,
            Description = Description,
            CategoryId = Category
        };
        await _subCategoryService.UpdateSubCategory(subCategory);
    }
}
