using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductById(Guid id);
    Task CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Guid id);

    Task<int> GetProductsCount();
}
