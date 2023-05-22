using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;

namespace ECommerce.Infrastructure.Repositories;

internal class OrderRepository : GenericRepository<Order, Guid>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }
}
