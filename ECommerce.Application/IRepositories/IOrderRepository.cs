using ECommerce.Application.IGenericRepositories;
using ECommerce.Core.Entities;

namespace ECommerce.Application.IRepositories;

public interface IOrderRepository : IGenericRepository<Order, Guid>
{
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
    Task<decimal> GetTotalSpendByUserIdAsync(Guid userId);
    Task<int> getTotalProductBoughtByUserIdAsync(Guid userId);
}
