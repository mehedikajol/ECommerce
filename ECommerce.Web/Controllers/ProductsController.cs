using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Core.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Runtime;

namespace ECommerce.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly FileStorageSettings _settings;

        public ProductsController(
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

        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductById(id);
            product.ImageUrl = Request.Scheme + "://" + Request.Host + _settings.DirectoryName
                + "/" + product.ImageUrl?.Replace('\\', '/');
            return new JsonResult(product);
        }
    }
}
