﻿using Autofac;
using ECommerce.Core.Enums;
using ECommerce.Core.Exceptions;
using ECommerce.Web.Areas.Admin.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Web.Areas.Admin.Controllers;

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

    public async Task<IActionResult> Index()
    {
        var model = new CategoryListModel();
        model.ResolveDependency(_scope);
        await model.LoadModelData();
        return View(model);
    }

    public IActionResult Create()
    {
        var model = new CategoryCreateModel();
        model.ResolveDependency(_scope);
        var mainCategories = from MainCategory s in Enum.GetValues(typeof(MainCategory))
                             select new { Id = s.GetHashCode(), Name = s.ToString() };
        ViewData["MainCategories"] = new SelectList(mainCategories, "Id", "Name");
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryCreateModel model)
    {
        var mainCategories = from MainCategory s in Enum.GetValues(typeof(MainCategory))
                             select new { Id = s.GetHashCode(), Name = s.ToString() };
        ViewData["MainCategories"] = new SelectList(mainCategories, "Id", "Name");

        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);
            try
            {
                await model.Create();
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

    public async Task<IActionResult> Edit(Guid id)
    {
        var model = new CategoryEditModel();
        model.ResolveDependency(_scope);
        try
        {
            await model.LoadData(id);
            var mainCategories = from MainCategory s in Enum.GetValues(typeof(MainCategory))
                                 select new { Id = s.GetHashCode(), Name = s.ToString() };
            ViewData["MainCategories"] = new SelectList(mainCategories, "Id", "Name");
            if (model.IsValidItem)
            {
                return View(model);
            }
        }
        catch (NotFoundException)
        {
            return Redirect(url: "/Errors/Notfound");
        }
        catch (Exception)
        {
            return Redirect(url: "/Errors/InternalServerError");
        }

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryEditModel model)
    {
        var mainCategories = from MainCategory s in Enum.GetValues(typeof(MainCategory))
                             select new { Id = s.GetHashCode(), Name = s.ToString() };
        ViewData["MainCategories"] = new SelectList(mainCategories, "Id", "Name");

        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);
            try
            {
                await model.UpdateCategory();
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicatePropertyException ex)
            {
                ModelState.AddModelError("", ex.Message);
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
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var model = new CategoryListModel();
        model.ResolveDependency(_scope);
        try
        {
            await model.DeleteCategory(id);
            return new JsonResult("Deleted");
        }
        catch (Exception ex)
        {
            return new JsonResult(ex.Message);
        }
    }
}
