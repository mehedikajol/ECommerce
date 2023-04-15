using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;

namespace ECommerce.Web.Areas.Admin.Models.Categories;

public class CategoryListModel
{
    private ICategoryService _categoryService;
    private ILifetimeScope _scope;

    public CategoryListModel()
    {
        
    }
    public CategoryListModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    internal void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _categoryService = _scope.Resolve<ICategoryService>();
    }

    public IEnumerable<Category> Categories { get; set; }

    public async void LoadModelData()
    {
        Categories = await _categoryService.GetAllCategories();
    }

}
