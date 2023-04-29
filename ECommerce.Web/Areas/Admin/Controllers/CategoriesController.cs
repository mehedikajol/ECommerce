using Autofac;
using ECommerce.Web.Areas.Admin.Models.Categories;
using ECommerce.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriesController : BaseController
{
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(
        ILifetimeScope scope, 
        ILogger<CategoriesController> logger) 
            : base(scope)
    {
        _logger = logger;
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

    public async Task<IActionResult> Edit(Guid id)
    {
        var model = new CategoryEditModel();
        model.ResolveDependency(_scope);
        await model.LoadData(id);
        if (model.IsValidItem)
        {
            return View(model);
        }
        else
        {
            return Redirect(url: "/Home/NotFound");
        }
        
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryEditModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);
            await model.UpdateCategory();
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}
