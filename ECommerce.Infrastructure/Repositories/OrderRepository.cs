using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Core.Enums;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

internal class OrderRepository : GenericRepository<Order, Guid>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Order>> GetAllEntities()
    {
        return await _dbSet
            .Include(o => o.OrderDetails)
            .ToListAsync();
    }

    public override async Task<Order> GetEntityById(Guid id)
    {
        return await _dbSet
            .Include(o => o.OrderDetails)
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderDetails)
            .OrderByDescending(o => o.InsertedDate)
            .ToListAsync();
    }

    public async Task<int> GetTotalOrderCountByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(o => o.UserId == userId)
            .CountAsync();
    }

    public async Task<int> GetTotalPendingOrdersCountByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(o => o.UserId == userId && (o.OrderStatus == OrderStatus.Processing || o.OrderStatus == OrderStatus.Shipping))
            .CountAsync();
    }

    public async Task<int> getTotalProductBoughtByUserIdAsync(Guid userId)
    {
        var orders = await _dbSet
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderDetails)
            .ToListAsync();
        var totalCount = 0;
        foreach(var order in orders)
        {
            totalCount += order.OrderDetails.Count();
        }
        return totalCount;
    }

    public async Task<decimal> GetTotalSpendByUserIdAsync(Guid userId)
    {
        var orders = await _dbSet
            .Where(o => o.UserId == userId)
            .ToListAsync();
        decimal totalCost = 0;
        foreach(var order in orders)
        {
            totalCost += order.TotalCost;
        }

        return totalCost;
    }
}
