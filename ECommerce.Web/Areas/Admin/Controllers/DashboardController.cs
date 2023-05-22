using Autofac;
using ECommerce.Application.IServices;
using ECommerce.Web.Areas.Admin.Models.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public DashboardController(ILifetimeScope scope,
            IUserProfileService userProfileService,
            IProductService productService,
            IOrderService orderService) 
            : base(scope)
        {
            _userProfileService = userProfileService;
            _productService = productService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardModel
            {
                Products = await _productService.GetProductsCount(),
                Users = await _userProfileService.GetUsersCount(),
                CompletedOrders = await _orderService.GetCompletedOrdersCount(),
                ProcessingOrders = await _orderService.GetProcessingOrdersCount()
            };

            return View(model);
        }
    }
}
