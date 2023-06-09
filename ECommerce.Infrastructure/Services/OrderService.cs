﻿using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Enums;
using ECommerce.Core.Exceptions;
using Mapster;
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

            var item = order.Adapt<Order>();
            item.PaymentMethod = (int)order.PaymentMethod;
            item.OrderStatus = (int)order.OrderStatus;
            item.OrderDate = order.InsertedDate;

            orders.Add(item);
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

        var order = orderEntitiy.Adapt<Order>();
        order.PaymentMethod = (int)orderEntitiy.PaymentMethod;
        order.OrderStatus = (int)orderEntitiy.OrderStatus;
        order.OrderDate = orderEntitiy.InsertedDate;

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

        var orderEntity = order.Adapt<EO.Order>();
        orderEntity.OrderStatus = OrderStatus.Processing;
        orderEntity.PaymentMethod = (PaymentMethod)order.PaymentMethod;

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

    public async Task<IEnumerable<Order>> GetAllOrdersByUserId(Guid userId)
    {
        var orderEntities = await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId);
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

            var item = order.Adapt<Order>();
            item.PaymentMethod = (int)order.PaymentMethod;
            item.OrderStatus = (int)order.OrderStatus;
            item.OrderDate = order.InsertedDate;

            orders.Add(item);
        }

        return orders;
    }

    public async Task<int> GetTotalOrderCountByUserIdAsync(Guid userId)
    {
        return await _unitOfWork.Orders
            .GetCount(o => o.UserId == userId);
    }

    public async Task<int> GetTotalCompletedOrderCountByUserIdAsync(Guid userId)
    {
        return await _unitOfWork.Orders
            .GetCount(o => o.UserId == userId && o.OrderStatus == OrderStatus.Completed);
    }

    public async Task<decimal> GetTotalSpendByUserIdAsync(Guid userId)
    {
        return await _unitOfWork.Orders.GetTotalSpendByUserIdAsync(userId);
    }

    public async Task<int> getTotalProductBoughtByUserIdAsync(Guid userId)
    {
        return await _unitOfWork.Orders.getTotalProductBoughtByUserIdAsync(userId);
    }

    public async Task<int> GetTotalPendingOrdersCountByUserIdAsync(Guid userId)
    {
        return await _unitOfWork.Orders
            .GetCount(o => o.UserId == userId && (o.OrderStatus == OrderStatus.Processing || o.OrderStatus == OrderStatus.Shipping));
    }
}
