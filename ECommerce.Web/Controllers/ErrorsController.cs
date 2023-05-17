using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers;

public class ErrorsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult NotFound()
    {
        return View();
    }

    public IActionResult InternalServerError()
    {
        return View();
    }
}
