using ECommerce.Application.BusinessEntities;

namespace ECommerce.Web.Areas.Admin.Models.Orders;

public class OrderListModel
{
    public List<OrderViewModel> Orders { get; set; }
}
