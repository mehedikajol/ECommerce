using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Web.Helpers;
using ECommerce.Web.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly FileStorageSettings _settings;

        public ShopController(
            IProductService productService,
            IOptions<FileStorageSettings> options)
        {
            _productService = productService;
            _settings = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ProductListModel();
            model.Products = await _productService.GetAllProducts();
            foreach(var product in model.Products)
            {
                product.ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl);
            }
            return View(model);
        }

        public async Task<IActionResult> ViewProduct(Guid id)
        {
            var product = await _productService.GetProductById(id);
            product.ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl);
            var model = new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SKU = product.SKU,
                ImageUrl = product.ImageUrl
            };

            return View(model);
        }

        public async Task<IActionResult> GetProductJson(Guid id)
        {
            var product = await _productService.GetProductById(id);
            product.ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl);
            return new JsonResult(product);
        }
    }
}
