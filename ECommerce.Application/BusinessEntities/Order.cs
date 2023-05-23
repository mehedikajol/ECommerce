namespace ECommerce.Application.BusinessEntities;

public class Order
{
    public Guid Id { get; set; }
    public decimal TotalCost { get; set; }
    public Guid UserId { get; set; }
    public string ReviewedBy { get; set; }
    public string ShippingAddress { get; set; }
    public int PaymentMethod { get; set; }
    public int OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
}

public class OrderDetail
{
    public Guid ProductId { get; set; }
}
