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

    public IActionResult Create()
    {
        var model = new CategoryCreateModel();
        model.ResolveDependency(_scope);
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);
            await model.Create();
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}
