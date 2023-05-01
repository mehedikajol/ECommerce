using Autofac;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
