using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Areas.Admin.Models.Categories;

public class CategoryEditModel
{
    private ICategoryService _categoryService;
    private ILifetimeScope _scope;

    public CategoryEditModel()
    {

    }

    public CategoryEditModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    internal void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _categoryService = _scope.Resolve<ICategoryService>();
    }

    public Guid Id { get; set; }

    [Required, MaxLength(50, ErrorMessage = "Name can't be more than 50 characters!")]
    public string Name { get; set; }

    [MaxLength(250, ErrorMessage = "Description can't be more than 250 characters!")]
    public string Description { get; set; }

    [Required]
    public int MainCategory { get; set; }

    public bool IsValidItem { get; set; }

    internal async Task LoadData(Guid id)
    {
        var category = await _categoryService.GetCategoryById(id);
        if(category is not null)
        {
            Name = category.Name;
            Description = category.Description;
            MainCategory = category.MainCategory;
            IsValidItem = true;
        }
        else
        {
            IsValidItem = false; 
        }
    }

    internal async Task UpdateCategory()
    {
        var category = new Category
        {
            Id = Id,
            Name = Name,
            Description = Description,
            MainCategory = MainCategory
        };
        await _categoryService.UpdateCategory(category);
    }
}
