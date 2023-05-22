using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Core.Enums;
using ECommerce.Web.Extensions;
using ECommerce.Web.Models.Checkout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ECommerce.Web.Controllers;

[Authorize]
public class CheckoutController : Controller
{
    private readonly IProductService _productService;
    private readonly IUserProfileService _userService;
    private readonly IOrderService _orderService;
    private readonly FileStorageSettings _settings;

    public CheckoutController(
        IProductService productService,
        IUserProfileService userService,
        IOrderService orderService,
        IOptions<FileStorageSettings> options)
    {
        _productService = productService;
        _userService = userService;
        _orderService = orderService;
        _settings = options.Value;
    }

    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userInfo = await _userService.GetUserProfileByIdentityId(new Guid(userId));
        var model = new CheckoutModel();
        model.User = new CheckoutUserModel
        {
            Name = userInfo.FirstName + " " + userInfo.LastName,
            Email = userInfo.Email,
            Phone = userInfo.Phone,
            Address = userInfo.Address,
        };

        var paymentMethods = from PaymentMethod s in Enum.GetValues(typeof(PaymentMethod))
                             select new { Id = s.GetHashCode(), Name = s.GetDisplayName() };
        ViewData["PaymentMethods"] = new SelectList(paymentMethods, "Id", "Name");

        return View(model);
    }

    public async Task<IActionResult> ConfirmOrder(int option)
    {
        var cartCookie = Request.Cookies["CartProducts"]?.ToString();
        var cartProducts = cartCookie?.Split("---").ToList();
        cartProducts?.RemoveAll(guid => !Guid.TryParse(guid, out _));

        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userInfo = await _userService.GetUserProfileByIdentityId(new Guid(userId));

        var orderDetails = new List<OrderDetail>();
        var totalCost = 0;

        foreach(var product in cartProducts)
        {
            var productEntity = await _productService.GetProductById(new Guid(product));
            totalCost += (int)productEntity.Price;

            orderDetails.Add(new OrderDetail
            {
                ProductId = new Guid(product)
            });
        }

        if(totalCost == 0)
        {
            return RedirectToAction(nameof(Index));
        }

        var order = new Order
        {
            UserId = new Guid(userId),
            PaymentMethod  = option,
            TotalCost = totalCost,
            OrderDetails = orderDetails
        };

        await _orderService.CreateOrder(order);

        return RedirectToAction(nameof(Success));
    }

    public IActionResult Success()
    {
        Response.Cookies.Delete("CartProducts");
        return View();
    }
}
