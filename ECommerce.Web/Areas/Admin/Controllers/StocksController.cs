using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Web.Areas.Admin.Models.Stocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Web.Areas.Admin.Controllers
{
    public class StocksController : BaseController
    {
        private readonly ILogger<StocksController> _logger;
        private readonly IStockService _stockService;
        private readonly IProductService _productService;

        public StocksController(
            ILifetimeScope scope,
            ILogger<StocksController> logger,
            IStockService stockService,
            IProductService productService) : base(scope)
        {
            _logger = logger;
            _stockService = stockService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var stockObjects = await _stockService.GetAllStocks();
            var model = new StockListModel();
            var stocks = new List<Stock>();
            foreach (var stock in stockObjects)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    StockAmout = stock.StockAmout,
                    ProductId = stock.ProductId,
                    ProductName = stock.ProductName,
                    CategoryName = stock.CategoryName,
                });
            }
            model.Stocks = stocks;

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new StockCreateModel();
            var products = await _productService.GetAllProducts();
            ViewData["Products"] = new SelectList(products, "Id", "Name");
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockCreateModel model)
        {
            var stock = new Stock();
            stock.ProductId = model.ProductId;
            stock.StockAmout = model.StockAmout;

            await _stockService.CreateStock(stock);

            return RedirectToAction(nameof(Index));
        }
    }
}
