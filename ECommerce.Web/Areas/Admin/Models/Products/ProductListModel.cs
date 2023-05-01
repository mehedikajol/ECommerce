using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;

namespace ECommerce.Web.Areas.Admin.Models.Products;

public class ProductListModel
{
    private IProductService _productService;
    private ILifetimeScope _scope;

    public ProductListModel()
    {
    }

    public ProductListModel(IProductService productService)
    {
        _productService = productService;
    }

    internal void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _productService = _scope.Resolve<IProductService>();
    }

    public IEnumerable<Product> Products { get; set; }

    public async Task LoadModelData()
    {
        Products = await _productService.GetAllProducts();
    }

    public async Task DeleteProduct(Guid id)
    {
        await _productService.DeleteProduct(id);
    }
}
