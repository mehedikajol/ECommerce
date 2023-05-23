using Autofac;
using ECommerce.Application.IServices;
using ECommerce.Core.Enums;
using ECommerce.Web.Areas.Admin.Models.Orders;
using ECommerce.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers;

public class OrdersController : BaseController
{
    private readonly IOrderService _orderService;
    private readonly IUserProfileService _userProfileService;

    public OrdersController(
        ILifetimeScope scope,
        IOrderService orderService,
        IUserProfileService userProfileService) 
        : base(scope)
    {
        _orderService = orderService;
        _userProfileService = userProfileService;
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
        return View();
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        return View();
    }
}
