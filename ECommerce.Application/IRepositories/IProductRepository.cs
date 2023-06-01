using ECommerce.Application.IGenericRepositories;
using ECommerce.Core.Entities;

namespace ECommerce.Application.IRepositories;

public interface IProductRepository : IGenericRepository<Product, Guid>
{
    Task<IEnumerable<Product>> GetFilteredProductsAsync(string searchTerm = null, int sortValue = 0);
}
