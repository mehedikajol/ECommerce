using ECommerce.Application.IGenericRepositories;
using ECommerce.Core.Entities;

namespace ECommerce.Application.IRepositories;

public interface IProductRepository : IGenericRepository<Product, Guid>
{
    Task<(IEnumerable<Product> products, int totalCount, int currentCount)> GetFilteredProductsAsync(string searchTerm = null, int sortValue = 0, int pageSize = 12, int currentPage = 1);
}
