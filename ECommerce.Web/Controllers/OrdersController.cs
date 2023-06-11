using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers;

[Authorize]
public class OrdersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Details(Guid id)
    {
        return View();
    }
}
