using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Web.Helpers;
using ECommerce.Web.Models.Checkout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.Web.Controllers;

public class CheckoutController : Controller
{
    private readonly IProductService _productService;
    private readonly FileStorageSettings _settings;

    public CheckoutController(
        IProductService productService,
        IOptions<FileStorageSettings> options)
    {
        _productService = productService;
        _settings = options.Value;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var cartCookie = Request.Cookies["CartProducts"]?.ToString();
        var cartProducts = cartCookie?.Split("---").ToList();
        cartProducts?.RemoveAll(guid => !Guid.TryParse(guid, out _));

        var products = new List<CheckoutProductModel>();
        if (cartProducts is not null)
        {
            foreach (var item in cartProducts)
            {
                var product = await _productService.GetProductById(new Guid(item));
                products.Add(new CheckoutProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl)
                });
            }
        }
        var model = new CheckoutModel();
        model.Products = products;

        return View(model);
    }
}
