using Autofac;
using ECommerce.Web.Areas.Admin.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Web.Areas.Admin.Controllers;

public class ProductsController : BaseController
{
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(
        ILifetimeScope scope,
        ILogger<ProductsController> logger) : base(scope)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var model = new ProductListModel();
        model.ResolveDependency(_scope);
        await model.LoadModelData();
        return View(model);
    }

    public async Task<IActionResult> CreateAsync()
    {
        var model = new ProductCreateModel();
        model.ResolveDependency(_scope);
        var subCategories = await model.LoadSubCategories();
        ViewData["SubCategories"] = new SelectList(subCategories, "Id", "Name");
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateModel model)
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
