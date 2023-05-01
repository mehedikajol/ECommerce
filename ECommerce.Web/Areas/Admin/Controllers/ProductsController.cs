using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers;

public class ProductsController : BaseController
{
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(
        ILifetimeScope scope, 
        ILogger<ProductsController> logger) : base(scope)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
}
