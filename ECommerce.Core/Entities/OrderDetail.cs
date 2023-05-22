namespace ECommerce.Core.Entities;

public class OrderDetail
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
}
