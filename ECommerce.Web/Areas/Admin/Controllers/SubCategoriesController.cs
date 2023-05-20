using Autofac;
using ECommerce.Core.Exceptions;
using ECommerce.Web.Areas.Admin.Models.SubCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Web.Areas.Admin.Controllers;

public class SubCategoriesController : BaseController
{
    private readonly ILogger<SubCategoriesController> _logger;
    public SubCategoriesController(
        ILifetimeScope scope,
        ILogger<SubCategoriesController> logger) 
        : base(scope)
    {
        _logger = logger;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var model = new SubCategoryListModel();
        model.ResolveDependency(_scope);
        await model.LoadModelData();
        return View(model);
    }

    public async Task<IActionResult> CreateAsync()
    {
        var model = new SubCategoryCreateModel();
        model.ResolveDependency(_scope);
        var categories = await model.LoadCategories();
        ViewData["Categories"] = new SelectList(categories, "Id", "Name");
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SubCategoryCreateModel model)
    {
        model.ResolveDependency(_scope);
        var categories = await model.LoadCategories();
        ViewData["Categories"] = new SelectList(categories, "Id", "Name");

        if (ModelState.IsValid)
        {
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
        var model = new SubCategoryEditModel();
        model.ResolveDependency(_scope);
        try
        {
            var categories = await model.LoadCategories();
            ViewData["Categories"] = new SelectList(categories, "Id", "Name");
            await model.LoadData(id);
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
    public async Task<IActionResult> Edit(SubCategoryEditModel model)
    {
        model.ResolveDependency(_scope);
        var categories = await model.LoadCategories();
        ViewData["Categories"] = new SelectList(categories, "Id", "Name");

        if (ModelState.IsValid)
        {
            try
            {
                await model.UpdateSubCategory();
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
        var model = new SubCategoryListModel();
        model.ResolveDependency(_scope);
        try
        {
            await model.DeleteSubCategory(id);
            return new JsonResult("Deleted");
        }
        catch (Exception ex)
        {
            return new JsonResult(ex.Message);
        }
    }
}
