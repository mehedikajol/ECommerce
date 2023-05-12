using ECommerce.Application.IServices;
using ECommerce.Core.Common;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewProduct(Guid id)
        {
            var product = await _productService.GetProductById(id);
            product.ImageUrl = Request.Scheme + "://" + Request.Host + _settings.DirectoryName
                + "/" + product.ImageUrl?.Replace('\\', '/');
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
    }
}
