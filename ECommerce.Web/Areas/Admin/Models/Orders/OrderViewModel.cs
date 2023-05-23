namespace ECommerce.Web.Areas.Admin.Models.Orders;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string Phone { get; set; }
    public decimal TotalCost { get; set; }
    public string PaymentMethod { get; set; }
    public string OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShippingAddress { get; set; }

    public ICollection<OrderProductModel> Products { get; set; }
}

public class OrderProductModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}
