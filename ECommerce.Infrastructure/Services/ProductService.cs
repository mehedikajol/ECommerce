using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;

namespace ECommerce.Infrastructure.Services;

internal class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        var productEntities = await _unitOfWork.Products.GetAllEntities();
        var products = new List<Product>();
        foreach (var product in productEntities)
        {
            products.Add(new Product
            {
                Id = product.Id,
                Name= product.Name,
                Description = product.Description,
                SKU = product.SKU,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                SubCategoryId = product.SubCategoryId,
                SubCategoryName = product.SubCategory.Name
            });
        }
        return products;
    }

    public Task<Product> GetProductById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProduct(Guid id)
    {
        throw new NotImplementedException();
    }

}
