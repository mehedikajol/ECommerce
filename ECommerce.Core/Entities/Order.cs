using ECommerce.Core.Entities.Base;
using ECommerce.Core.Enums;

namespace ECommerce.Core.Entities;

public class Order : AuditableEntity
{
    public decimal TotalCost { get; set; }
    public Guid UserId { get; set; }
    public string ReviewedBy { get; set; }
    public string ShippingAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
