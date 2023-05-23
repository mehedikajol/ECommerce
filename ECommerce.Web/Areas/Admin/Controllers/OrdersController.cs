using Autofac;
using ECommerce.Application.IServices;
using ECommerce.Core.Enums;
using ECommerce.Web.Areas.Admin.Models.Orders;
using ECommerce.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

namespace ECommerce.Web.Areas.Admin.Controllers
{
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
            model.Orders = new List<OrderModel>();
            foreach (var order in orders)
            {
                var user = await _userProfileService.GetUserProfileByIdentityId(order.UserId);
                model.Orders.Add(new OrderModel
                {
                    UserEmail = user.Email,
                    UserName = user.FirstName + " " + user.LastName,
                    TotalCost = order.TotalCost,
                    PaymentMethod = Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>().Where(x => x.GetHashCode() == order.PaymentMethod).FirstOrDefault().GetDisplayName(),
                    OrderStatus = Enum.GetName(typeof(OrderStatus), order.OrderStatus)

                }); ; ;
            }

            return View(model);
        }
    }
}
