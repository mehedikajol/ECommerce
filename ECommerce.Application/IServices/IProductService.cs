using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<IEnumerable<Product>> GetFilteredProducts(string searchTerm = null, int sortValue = 0);
    Task<Product> GetProductById(Guid id);
    Task CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Guid id);

    Task<int> GetProductsCount();
}
