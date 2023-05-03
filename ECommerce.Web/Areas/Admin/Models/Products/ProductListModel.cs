using ECommerce.Application.BusinessEntities;

namespace ECommerce.Web.Areas.Admin.Models.Products;

public class ProductListModel
{
    public IEnumerable<Product> Products { get; set; }
}
