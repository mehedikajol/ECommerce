﻿using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<Order> GetOrderById(Guid id);
    Task CreateOrder(Order order);
    Task UpdateOrder(Order order);
    Task DeleteOrder(Guid id);

    Task<int> GetCompletedOrdersCount();
    Task<int> GetProcessingOrdersCount();
    Task<IEnumerable<Order>> GetAllOrdersByUserId(Guid userId);

    Task<int> GetTotalOrderCountByUserIdAsync(Guid userId);
    Task<int> GetTotalCompletedOrderCountByUserIdAsync(Guid userId);
    Task<decimal> GetTotalSpendByUserIdAsync(Guid userId);
    Task<int> getTotalProductBoughtByUserIdAsync(Guid userId);
    Task<int> GetTotalPendingOrdersCountByUserIdAsync(Guid userId);
}
