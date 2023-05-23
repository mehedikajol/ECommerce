using Autofac;
using ECommerce.Application.IServices;
using ECommerce.Core.Enums;
using ECommerce.Core.Exceptions;
using ECommerce.Web.Areas.Admin.Models.Orders;
using ECommerce.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ECommerce.Web.Areas.Admin.Controllers;

public class OrdersController : BaseController
{
    private readonly IOrderService _orderService;
    private readonly IUserProfileService _userProfileService;
    private readonly IProductService _productService;

    public OrdersController(
        ILifetimeScope scope,
        IOrderService orderService,
        IUserProfileService userProfileService,
        IProductService productService) 
        : base(scope)
    {
        _orderService = orderService;
        _userProfileService = userProfileService;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetAllOrders();
        var model = new OrderListModel();
        model.Orders = new List<OrderViewModel>();
        foreach (var order in orders)
        {
            var user = await _userProfileService.GetUserProfileByIdentityId(order.UserId);
            model.Orders.Add(new OrderViewModel
            {
                Id = order.Id,
                UserEmail = user.Email,
                UserName = user.FirstName + " " + user.LastName,
                TotalCost = order.TotalCost,
                OrderDate = order.OrderDate,
                PaymentMethod = ((PaymentMethod)order.PaymentMethod).GetDisplayName(),
                OrderStatus = ((OrderStatus)order.OrderStatus).ToString()
            }); 
        }

        return View(model);
    }

    public async Task<IActionResult> View(Guid id)
    {
        try
        {
            var order = await _orderService.GetOrderById(id);
            var user = await _userProfileService.GetUserProfileByIdentityId(order.UserId);

            var model = new OrderViewModel();
            var products = new List<OrderProductModel>();

            foreach(var item in order.OrderDetails)
            {
                var product = await _productService.GetProductById(item.ProductId);
                products.Add(new OrderProductModel
                {
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.SubCategoryName
                });
            };

            model.Id = order.Id;
            model.UserName = user.FirstName + " " + user.LastName;
            model.UserEmail = user.Email;
            model.Phone = user.Phone;
            model.TotalCost = order.TotalCost;
            model.OrderDate = order.OrderDate;
            model.PaymentMethod = ((PaymentMethod)order.PaymentMethod).GetDisplayName();
            model.OrderStatus = ((OrderStatus)order.OrderStatus).ToString();
            model.ShippingAddress = order.ShippingAddress;
            model.Products = products;

            return View(model);
        }
        catch (NotFoundException ex)
        {
            Log.Error(ex.Message, ex);
            return Redirect(url: "/Errors/Notfound");
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
            return Redirect(url: "/Errors/InternalServerError");
        }
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        return View();
    }
}
