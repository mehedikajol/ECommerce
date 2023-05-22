namespace ECommerce.Core.Entities;

public class OrderDetail
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}
