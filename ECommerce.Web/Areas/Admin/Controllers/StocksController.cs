using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Core.Exceptions;
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
            var products = await _productService.GetAllProducts();
            ViewData["Products"] = new SelectList(products, "Id", "Name");

            if (ModelState.IsValid)
            {
                try
                {
                    var stock = new Stock();
                    stock.ProductId = model.ProductId;
                    stock.StockAmout = model.StockAmout;

                    await _stockService.CreateStock(stock);

                    return RedirectToAction(nameof(Index));
                }
                catch(InvalidInputException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
                catch (DuplicatePropertyException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var model = new StockEditModel();
                var entity = await _stockService.GetStockById(id);

                model.Id = id;
                model.StockAmout = entity.StockAmout;
                model.ProductId = entity.ProductId;

                var products = await _productService.GetAllProducts();
                ViewData["Products"] = new SelectList(products, "Id", "Name");

                return View(model);
            }
            catch (NotFoundException)
            {
                return Redirect(url: "/Errors/Notfound");
            }
            catch (Exception)
            {
                return Redirect(url: "/Errors/InternalServerError");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StockEditModel model)
        {
            var products = await _productService.GetAllProducts();
            ViewData["Products"] = new SelectList(products, "Id", "Name");
            if (ModelState.IsValid)
            {
                if (model.AddStock > 0 && model.RemoveStock > 0)
                {
                    ModelState.AddModelError("", "You can't add and remove from stock at a time.");
                    return View(model);
                }
                if (model.RemoveStock > model.StockAmout)
                {
                    ModelState.AddModelError("", "You can't remove more than stock amout.");
                    return View(model);
                }
                try
                {
                    var stock = new Stock
                    {
                        Id = model.Id,
                        StockAmout = model.StockAmout + model.AddStock - model.RemoveStock,
                        ProductId = model.ProductId,
                    };
                    await _stockService.UpdateStock(stock);
                    return RedirectToAction(nameof(Index));
                }
                catch(InvalidInputException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
                catch(DuplicatePropertyException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
                catch (NotFoundException)
                {
                    return Redirect(url: "/Errors/Notfound");
                }
                catch (Exception)
                {
                    return Redirect(url: "/Errors/InternalServerError");
                }
            }
            return View(model);
        }
    }
}
