using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Core.Enums;
using ECommerce.Web.Extensions;
using ECommerce.Web.Helpers;
using ECommerce.Web.Helpers.RequestModels;
using ECommerce.Web.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var sortValues = from ProductSortValue s in Enum.GetValues(typeof(ProductSortValue))
                             select new { Id = s.GetHashCode(), Name = s.GetDisplayName() };
            ViewData["ProductSortValues"] = new SelectList(sortValues, "Id", dataTextField: "Name");
            return View();
        }

        public async Task<IActionResult> ViewProduct(Guid id)
        {
            var product = await _productService.GetProductById(id);
            product.ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl);
            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SKU = product.SKU,
                ImageUrl = product.ImageUrl
            };

            return View(model);
        }

        public async Task<IActionResult> GetProductsJson(ProductFilterRequestModel model)
        {
            var result = await _productService.GetFilteredProducts(model.SearchString, model.SortValue, model.PageSize, model.CurrentPage);
            foreach (var product in result.products)
            {
                product.ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl);
            }

            var output = new
            {
                products = result.products,
                totalCount = result.totalCount,
                currentCount = result.currentCount
            };


            return new JsonResult(output);
        }

        public async Task<IActionResult> GetProductJson(Guid id)
        {
            var product = await _productService.GetProductById(id);
            product.ImageUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, product.ImageUrl);
            return new JsonResult(product);
        }
    }
}
