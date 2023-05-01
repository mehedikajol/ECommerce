using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;

namespace ECommerce.Web.Areas.Admin.Models.SubCategories;

public class SubCategoryListModel
{
    private ISubCategoryService _subCategoryService;
    private ILifetimeScope _scope;

    public SubCategoryListModel()
    {
    }

    public SubCategoryListModel(ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }

    internal void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _subCategoryService = _scope.Resolve<ISubCategoryService>();
    }

    public IEnumerable<SubCategory> Categories { get; set; }

    public async Task LoadModelData()
    {
        Categories = await _subCategoryService.GetAllSubCategories();
    }

    public async Task DeleteSubCategory(Guid id)
    {
        await _subCategoryService.DeleteSubCategory(id);
    }
}
