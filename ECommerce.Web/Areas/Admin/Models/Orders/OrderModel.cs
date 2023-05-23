namespace ECommerce.Web.Areas.Admin.Models.Orders;

public class OrderModel
{
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public decimal TotalCost { get; set; }
    public string PaymentMethod { get; set; }
    public string OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
}
