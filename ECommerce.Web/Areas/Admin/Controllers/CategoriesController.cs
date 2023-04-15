using Autofac;
using ECommerce.Web.Areas.Admin.Models.Categories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriesController : Controller
{
    private readonly ILifetimeScope _scope;
    public CategoriesController(ILifetimeScope scope)
    {
        _scope = scope;
    }

    public IActionResult Index()
    {
        var model = new CategoryListModel();
        model.ResolveDependency(_scope);
        model.LoadModelData();
        return View(model);
    }
}
