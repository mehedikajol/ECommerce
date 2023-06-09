﻿using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Core.Enums;
using ECommerce.Core.Exceptions;
using ECommerce.Web.Areas.Admin.Models.Products;
using ECommerce.Web.Extensions;
using ECommerce.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace ECommerce.Web.Areas.Admin.Controllers;

public class ProductsController : BaseController
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;
    private readonly ISubCategoryService _subCategoryService;
    private readonly IFileHandlerService _fileHandlerService;
    private readonly FileStorageSettings _settings;

    public ProductsController(
        ILifetimeScope scope,
        ILogger<ProductsController> logger,
        IProductService productService,
        ISubCategoryService subCategoryService,
        IFileHandlerService fileHandlerService,
        IOptions<FileStorageSettings> options) 
        : base(scope)
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
            entity.ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, entity.ImageUrl);
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
        var subCategories = await _subCategoryService.GetAllSubCategories();
        ViewData["SubCategories"] = new SelectList(subCategories, "Id", "Name");

        if (ModelState.IsValid)
        {
            try
            {
                if (model.ImageFile is not null)
                {
                    var bytes = await model.ImageFile.ToByteArray();
                    var imageUrl = await _fileHandlerService.SaveFileAsync(bytes, model.ImageFile.FileName, UploadImageType.ProductImage);
                    model.ImageUrl = imageUrl;
                }
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    SubCategoryId = model.Category,
                    ImageUrl = model.ImageUrl,
                    SKU = RandomStringGenerator.GenerateUppercaseRandomString(15),
                    Price = model.Price,
                };
                await _productService.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicatePropertyException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                return Redirect(url: "/Errors/InternalServerError");
            }
        }

        return View(model);
    }

    public async Task<IActionResult> View(Guid id)
    {
        try
        {
            var product = await _productService.GetProductById(id);
            var model = new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                Category = product.SubCategoryName,
                ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl),
                Price = product.Price,
            };
            return View(model);
        }
        catch (NotFoundException)
        {
            return Redirect(url: "/Errors/Notfound");
        }
        catch (Exception)
        {
            return Redirect(url: "/Errors/InternalServerError");
        }
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var subCategories = await _subCategoryService.GetAllSubCategories();
        ViewData["SubCategories"] = new SelectList(subCategories, "Id", "Name");

        try
        {
            var entity = await _productService.GetProductById(id);
            var model = new ProductEditModel();

            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Description = entity.Description;
            model.Price = entity.Price;
            model.Category = entity.SubCategoryId;

            return View(model);
        }
        catch (NotFoundException)
        {
            return Redirect(url: "/Errors/Notfound");
        }
        catch (Exception)
        {
            return Redirect(url: "/Errors/InternalServerError");
        }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductEditModel model)
    {
        var subCategories = await _subCategoryService.GetAllSubCategories();
        ViewData["SubCategories"] = new SelectList(subCategories, "Id", "Name");

        if (ModelState.IsValid)
        {
            try
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
            catch (NotFoundException)
            {
                return Redirect(url: "/Errors/Notfound");
            }
            catch(DuplicatePropertyException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                return Redirect(url: "/Errors/InternalServerError");
            }
        }
        
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _productService.DeleteProduct(id);
            return new JsonResult("Deleted");
        }
        catch (Exception ex)
        {
            return new JsonResult(ex.Message);
        }
    }
}
