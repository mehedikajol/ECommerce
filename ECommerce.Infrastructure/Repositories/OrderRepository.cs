using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
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
}
