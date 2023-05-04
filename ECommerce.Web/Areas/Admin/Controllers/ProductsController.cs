using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Core.Enums;
using ECommerce.Web.Areas.Admin.Models.Products;
using ECommerce.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Text;

namespace ECommerce.Web.Areas.Admin.Controllers;

public class ProductsController : BaseController
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;
    private readonly ISubCategoryService _subCategoryService;
    private readonly IFileHandlerService _fileHandlerService;
    private readonly ApplicationSettings _settings;

    public ProductsController(
        ILifetimeScope scope,
        ILogger<ProductsController> logger,
        IProductService productService,
        ISubCategoryService subCategoryService,
        IFileHandlerService fileHandlerService,
        IOptions<ApplicationSettings> options) : base(scope)
    {
        _logger = logger;
        _productService = productService;
        _subCategoryService = subCategoryService;
        _fileHandlerService = fileHandlerService;
        _settings = options.Value;
    }

    public async Task<IActionResult> Index()
    {
        var model = new ProductListModel();
        var entities = await _productService.GetAllProducts();
        foreach (var entity in entities)
        {
            if (entity.ImageUrl.StartsWith("P"))
            {
                var path = Path.Combine(Request.Path);
                entity.ImageUrl = Request.Scheme + "://" + Request.Host + "/" + entity.ImageUrl?.Replace('\\', '/');
            }
        }

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
            if (model.ImageFile is not null)
            {
                var bytes = await model.ImageFile.ToByteArray();
                var imageUrl = await _fileHandlerService.SaveFileAsync(bytes, model.ImageFile.FileName, UploadImageTypes.ProductImage);
                model.ImageUrl = imageUrl;
            }
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                SubCategoryId = model.Category,
                ImageUrl = model.ImageUrl, // https:/picsum.photos/200/300", // TODO: File will be uploaded and link saved
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
