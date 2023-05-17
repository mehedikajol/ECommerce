using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Exceptions;
using EO = ECommerce.Core.Entities;

namespace ECommerce.Infrastructure.Services;

internal class StockService : IStockService
{
    private readonly IUnitOfWork _unitOfWork;

    public StockService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Stock>> GetAllStocks()
    {
        var stockEntities = await _unitOfWork.Stocks.GetAllEntities();
        var stocks = new List<Stock>();
        foreach (var stock in stockEntities)
        {
            stocks.Add(new Stock
            {
                Id = stock.Id,
                StockAmout = stock.StockAmout,
                ProductId = stock.ProductId,
                ProductName = stock.Product.Name,
                CategoryName = stock.Product.SubCategory.Name
            });
        }
        return stocks;
    }

    public async Task<Stock> GetStockById(Guid id)
    {
        var stockEntity = await _unitOfWork.Stocks.GetEntityById(id);
        if (stockEntity is null)
            throw new NotFoundException("Stock not found.");

        var stock = new Stock
        {
            Id = stockEntity.Id,
            StockAmout = stockEntity.StockAmout,
            ProductId = stockEntity.ProductId,
            ProductName = stockEntity.Product.Name
        };
        return stock;
    }

    public async Task CreateStock(Stock stock)
    {
        var isProductUsed = await IsProductAlreadyUsed(stock.ProductId);
        if (isProductUsed)
            throw new DuplicatePropertyException("Product is already used in another stock.");

        if (stock.StockAmout < 1)
            throw new InvalidInputException("Stock amount should be greater than 0.");

        var stockEntity = new EO.Stock
        {
            StockAmout = stock.StockAmout,
            ProductId = stock.ProductId,
        };
        await _unitOfWork.Stocks.AddEntity(stockEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateStock(Stock stock)
    {
        var stockEntity = await _unitOfWork.Stocks.GetEntityById(stock.Id);
        if (stockEntity is null)
            throw new NotFoundException("Stock not found.");

        var isProductUsed = await IsProductAlreadyUsed(stock.ProductId, stock.Id);
        if (isProductUsed)
            throw new DuplicatePropertyException("Product is already used in another stock.");

        stockEntity.StockAmout = stock.StockAmout;
        stockEntity.ProductId = stock.ProductId;
        await _unitOfWork.Stocks.UpdateEntity(stockEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteStock(Guid id)
    {
        var stockEntity = await _unitOfWork.Stocks.GetEntityById(id);
        if (stockEntity is null)
            throw new NotFoundException("Stock not found.");

        await _unitOfWork.Stocks.DeleteEntityById(id);
        await _unitOfWork.CompleteAsync();
    }

    private async Task<bool> IsProductAlreadyUsed(Guid productId) =>
        await _unitOfWork.Stocks.GetCount(s => s.ProductId == productId) > 0;

    private async Task<bool> IsProductAlreadyUsed(Guid productId, Guid stockId) =>
        await _unitOfWork.Stocks.GetCount(s => s.ProductId == productId && s.Id != stockId) > 0;

}
