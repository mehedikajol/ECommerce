using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers;

public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Orders()
    {
        return View();
    }

    public IActionResult Details()
    {
        return View();
    }
}
