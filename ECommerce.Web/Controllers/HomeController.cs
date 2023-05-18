using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Web.Helpers;
using ECommerce.Web.Models;
using ECommerce.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace ECommerce.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly FileStorageSettings _settings;

    public HomeController(
        ILogger<HomeController> logger,
        IProductService productService,
        IOptions<FileStorageSettings> options)
    {
        _logger = logger;
        _productService = productService;
        _settings = options.Value;
    }

    public async Task<IActionResult> Index()
    {
        var test = Request.Cookies;
        var model = new HomeProductsListModel();
        model.LatestProducts = await _productService.GetAllProducts();

        foreach (var product in model.LatestProducts)
            product.ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl);
        model.LatestProducts = model.LatestProducts.OrderBy(p => p.Name).ToList();

        model.PopularProducts = await _productService.GetAllProducts();
        foreach (var product in model.PopularProducts)
            product.ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl);
        model.PopularProducts = model.PopularProducts.OrderBy(p => p.Price).ToList();

        model.TrendingProducts = await _productService.GetAllProducts();
        foreach (var product in model.TrendingProducts)
            product.ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl);
        model.TrendingProducts = model.TrendingProducts.OrderByDescending(p => p.Name).ToList();

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}