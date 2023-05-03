using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Web.Areas.Admin.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace ECommerce.Web.Areas.Admin.Controllers;

public class ProductsController : BaseController
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;
    private readonly ISubCategoryService _subCategoryService;

    public ProductsController(
        ILifetimeScope scope,
        ILogger<ProductsController> logger,
        IProductService productService,
        ISubCategoryService subCategoryService) : base(scope)
    {
        _logger = logger;
        _productService = productService;
        _subCategoryService = subCategoryService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new ProductListModel();
        var entities = await _productService.GetAllProducts();
        model.Products = entities;
        return View(model);
    }

    public async Task<IActionResult> CreateAsync()
    {
        var model = new ProductCreateModel();
        var subCategories = await _subCategoryService.GetAllSubCategories();
        ViewData["SubCategories"] = new SelectList(subCategories, "Id", "Name");
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                SubCategoryId = model.Category,
                ImageUrl = "https://picsum.photos/200/300", // TODO: File will be uploaded and link saved
                SKU = "5BLP4EWUCQ59ZTD", // TODO: Random string generator needed to generate SKU (15 Chars)
                Price = model.Price,
            };
            await _productService.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }
        var subCategories = await _subCategoryService.GetAllSubCategories();
        ViewData["SubCategories"] = new SelectList(subCategories, "Id", "Name");
        return View(model);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var subCategories = await _subCategoryService.GetAllSubCategories();
        ViewData["SubCategories"] = new SelectList(subCategories, "Id", "Name");

        var entity = await _productService.GetProductById(id);
        var model = new ProductEditModel();

        model.Id = entity.Id;
        model.Name = entity.Name;
        model.Description = entity.Description;
        model.Price = entity.Price;
        model.Category = entity.SubCategoryId;

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductEditModel model)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                SubCategoryId = model.Category
            };
            await _productService.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }
        var subCategories = await _subCategoryService.GetAllSubCategories();
        ViewData["SubCategories"] = new SelectList(subCategories, "Id", "Name");
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productService.DeleteProduct(id);
        return new JsonResult("Deleted");
    }
}
