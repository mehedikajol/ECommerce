using ECommerce.Application.BusinessEntities;

namespace ECommerce.Web.Areas.Admin.Models.Orders;

public class OrderListModel
{
    public List<OrderModel> Orders { get; set; }
}
