using ECommerce.Application.BusinessEntities;

namespace ECommerce.Web.Models.Shop;

public class ProductListModel
{
    public IEnumerable<Product> Products { get; set; }
}
