﻿using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<(IEnumerable<Product> products, int totalCount, int currentCount)> GetFilteredProducts(string searchTerm = null, int sortValue = 0, int pageSize = 12, int currentPage = 1);
    Task<Product> GetProductById(Guid id);
    Task CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Guid id);

    Task<int> GetProductsCount();
}
