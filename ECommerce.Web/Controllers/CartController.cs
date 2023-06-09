﻿using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Web.Helpers;
using ECommerce.Web.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly FileStorageSettings _settings;

    public CartController(
        IProductService productService,
        IOptions<FileStorageSettings> options)
    {
        _productService = productService;
        _settings = options.Value;
    }

    public async Task<IActionResult> Index()
    {
        var cartCookie = Request.Cookies["CartProducts"]?.ToString();
        var cartProducts = cartCookie?.Split("---").ToList();
        cartProducts?.RemoveAll(guid => !Guid.TryParse(guid, out _));

        var products = new List<CartProductModel>();
        if (cartProducts is not null)
        {
            foreach (var item in cartProducts)
            {
                var product = await _productService.GetProductById(new Guid(item));
                products.Add(new CartProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl)
                });
            }
        }
        var model = new CartProductListModel();
        model.Products = products;

        return View(model);
    }

    public async Task<IActionResult> GetCartProductJson(string cookie)
    {
        var cartProducts = cookie?.Split("---").ToList();
        cartProducts?.RemoveAll(guid => !Guid.TryParse(guid, out _));

        var products = new List<CartProductModel>();
        if (cartProducts is not null)
        {
            foreach (var item in cartProducts)
            {
                var product = await _productService.GetProductById(new Guid(item));
                products.Add(new CartProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl)
                });
            }
        }
        var model = new CartProductListModel();

        return new JsonResult(products);
    }
}
