using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers;

public class CheckoutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
