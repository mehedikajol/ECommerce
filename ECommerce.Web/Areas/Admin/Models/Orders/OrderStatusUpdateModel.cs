namespace ECommerce.Web.Areas.Admin.Models.Orders;

public class OrderStatusUpdateModel
{
    public string OrderId { get; set; }
    public int UpdatedStatus { get; set; }
}
