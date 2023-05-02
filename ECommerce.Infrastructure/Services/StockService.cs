using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
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
        var stockEntity = new EO.Stock
        {
            StockAmout = stock.StockAmout,
            ProductId = stock.ProductId,
        };
        await _unitOfWork.Stocks.AddEntity(stockEntity);
        await _unitOfWork.CompleteAsync();
    }

    public Task UpdateStock(Stock stock)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteStock(Guid id)
    {
        await _unitOfWork.Stocks.DeleteEntityById(id);
        await _unitOfWork.CompleteAsync();
    }
}
