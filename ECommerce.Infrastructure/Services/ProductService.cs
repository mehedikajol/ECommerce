using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Exceptions;
using Mapster;
using EO = ECommerce.Core.Entities;

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
            var item = product.Adapt<Product>();
            item.SubCategoryName = product.SubCategory.Name;

            products.Add(item);
        }
        return products;
    }

    public async Task<(IEnumerable<Product> products, int totalCount, int currentCount)> GetFilteredProducts(string searchTerm = null, int sortValue = 0, int pageSize = 12, int currentPage = 1)
    {
        var result = await _unitOfWork.Products.GetFilteredProductsAsync(searchTerm, sortValue, pageSize, currentPage);
        var products = new List<Product>();
        foreach (var product in result.products)
        {
            var item = product.Adapt<Product>();
            item.SubCategoryName = product.SubCategory.Name;

            products.Add(item);
        }

        return (products, result.totalCount, result.currentCount);
    }

    public async Task<Product> GetProductById(Guid id)
    {
        var productEntity = await _unitOfWork.Products.GetEntityById(id);
        if (productEntity is null)
            throw new NotFoundException("Product not found.");

        var product = productEntity.Adapt<Product>();
        product.SubCategoryName = productEntity.SubCategory.Name;

        return product;
    }

    public async Task CreateProduct(Product product)
    {
        var alreadyUsedName = await IsNameAlreadyUsed(product.Name);
        if (alreadyUsedName)
            throw new DuplicatePropertyException("Product name is already in use.");

        var productEntity = product.Adapt<EO.Product>();

        await _unitOfWork.Products.AddEntity(productEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateProduct(Product product)
    {
        var productEntity = await _unitOfWork.Products.GetEntityById(product.Id);
        if (productEntity is null)
            throw new NotFoundException("Product not found.");

        var alreadyUsedName = await IsNameAlreadyUsed(product.Name, product.Id);
        if (alreadyUsedName)
            throw new DuplicatePropertyException("Product name is already in use.");

        productEntity.Name = product.Name;
        productEntity.Description = product.Description;
        productEntity.Price = product.Price;
        productEntity.SubCategoryId = product.SubCategoryId;

        await _unitOfWork.Products.UpdateEntity(productEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteProduct(Guid id)
    {
        var productEntity = await _unitOfWork.Products.GetEntityById(id);
        if (productEntity is null)
            throw new NotFoundException("Product not found.");

        await _unitOfWork.Products.DeleteEntityById(id);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<int> GetProductsCount()
    {
        return await _unitOfWork.Products.GetCount();
    }

    private async Task<bool> IsNameAlreadyUsed(string name) =>
        await _unitOfWork.Products.GetCount(p => p.Name == name.Trim()) > 0;

    private async Task<bool> IsNameAlreadyUsed(string name, Guid productId) =>
        await _unitOfWork.Products.GetCount(p => p.Name == name.Trim() && p.Id != productId) > 0;
}
