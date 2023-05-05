using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController(ILifetimeScope scope) : base(scope)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
