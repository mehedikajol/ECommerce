using Autofac;
using ECommerce.Web.Areas.Admin.Models.Categories;
using ECommerce.Web.Areas.Admin.Models.SubCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class SubCategoriesController : BaseController
    {
        private readonly ILogger<SubCategoriesController> _logger;
        public SubCategoriesController(
            ILifetimeScope scope, 
            ILogger<SubCategoriesController> logger) : base(scope)
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
            var model = new SubCategoryEditModel();
            model.ResolveDependency(_scope);
            var categories = await model.LoadCategories();
            ViewData["Categories"] = new SelectList(categories, "Id", "Name");
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
        public async Task<IActionResult> Edit(SubCategoryEditModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.UpdateSubCategory();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
