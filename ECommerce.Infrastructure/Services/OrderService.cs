using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Enums;
using ECommerce.Core.Exceptions;
using EO = ECommerce.Core.Entities;

namespace ECommerce.Infrastructure.Services;

internal class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        var orderEntities = await _unitOfWork.Orders.GetAllEntities();
        var orders = new List<Order>();

        foreach (var order in orderEntities)
        {
            var orderDetails = new List<OrderDetail>();
            foreach (var detail in order.OrderDetails)
            {
                orderDetails.Add(new OrderDetail
                {
                    ProductId = detail.ProductId,
                });
            }

            orders.Add(new Order
            {
                Id = order.Id,
                ReviewedBy = order.ReviewedBy,
                UserId = order.UserId,
                TotalCost = order.TotalCost,
                ShippingAddress = order.ShippingAddress,
                PaymentMethod = (int)order.PaymentMethod,
                OrderStatus = (int)order.OrderStatus,
                OrderDetails = orderDetails,
                OrderDate = order.InsertedDate
            });
        }

        return orders;
    }

    public async Task<Order> GetOrderById(Guid id)
    {
        var orderEntitiy = await _unitOfWork.Orders.GetEntityById(id);
        if (orderEntitiy is null)
            throw new NotFoundException("Order not found.");

        var orderDetails = new List<OrderDetail>();
        foreach (var detail in orderEntitiy.OrderDetails)
        {
            orderDetails.Add(new OrderDetail
            {
                ProductId = detail.ProductId,
            });
        }

        var order = new Order
        {
            Id = orderEntitiy.Id,
            ReviewedBy = orderEntitiy.ReviewedBy,
            UserId = orderEntitiy.UserId,
            TotalCost = orderEntitiy.TotalCost,
            ShippingAddress = orderEntitiy.ShippingAddress,
            PaymentMethod = (int)orderEntitiy.PaymentMethod,
            OrderStatus = (int)orderEntitiy.OrderStatus,
            OrderDetails = orderDetails,
            OrderDate = orderEntitiy.InsertedDate
        };

        return order;
    }

    public async Task CreateOrder(Order order)
    {
        var orderDetails = new List<EO.OrderDetail>();
        foreach (var orderDetail in order.OrderDetails)
        {
            orderDetails.Add(new EO.OrderDetail
            {
                ProductId = orderDetail.ProductId,
            });
        }

        var orderEntity = new EO.Order
        {
            UserId = order.UserId,
            OrderStatus = OrderStatus.Processing,
            ShippingAddress = order.ShippingAddress,
            PaymentMethod = (PaymentMethod)order.PaymentMethod,
            TotalCost = order.TotalCost,
            OrderDetails = orderDetails
        };

        await _unitOfWork.Orders.AddEntity(orderEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateOrder(Order order)
    {
        var orderEntity = await _unitOfWork.Orders.GetEntityById(order.Id);
        orderEntity.OrderStatus = (OrderStatus)order.OrderStatus;
        orderEntity.ReviewedBy = order.ReviewedBy ?? orderEntity.ReviewedBy;

        await _unitOfWork.Orders.UpdateEntity(orderEntity);
        await _unitOfWork.CompleteAsync();
    }

    public Task DeleteOrder(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetCompletedOrdersCount()
    {
        return await _unitOfWork.Orders
            .GetCount(o => o.OrderStatus == OrderStatus.Completed);
    }

    public async Task<int> GetProcessingOrdersCount()
    {
        return await _unitOfWork.Orders
            .GetCount(o => o.OrderStatus == OrderStatus.Shipping || o.OrderStatus == OrderStatus.Processing);
    }
}
