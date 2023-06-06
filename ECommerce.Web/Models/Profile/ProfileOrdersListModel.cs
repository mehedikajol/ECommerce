namespace ECommerce.Web.Models.Profile;

public class ProfileOrdersListModel
{
    public List<ProfileOrderModel> Orders { get; set; } = new List<ProfileOrderModel>();
}

public class ProfileOrderModel
{
    public Guid Id { get; set; }
    public decimal TotalCost { get; set; }
    public Guid UserId { get; set; }
    public string ReviewedBy { get; set; }
    public string ShippingAddress { get; set; }
    public int PaymentMethod { get; set; }
    public int OrderStatus { get; set; }
    public string OrderStatusInWord { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
}

public class OrderDetail
{
    public Guid ProductId { get; set; }
}