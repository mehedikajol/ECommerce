using Autofac;
using ECommerce.Web.Areas.Admin.Models.Categories;
using ECommerce.Web.Areas.Admin.Models.SubCategories;
using Microsoft.AspNetCore.Mvc;

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
    }
}
