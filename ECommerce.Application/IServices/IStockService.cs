using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices;

public interface IStockService
{
    Task<IEnumerable<Stock>> GetAllStocks();
    Task<Stock> GetStockById(Guid id);
    Task CreateStock(Stock stock);
    Task UpdateStock(Stock stock);
    Task DeleteStock(Guid id);
}
