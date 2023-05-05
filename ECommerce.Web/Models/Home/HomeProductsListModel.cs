using ECommerce.Application.BusinessEntities;

namespace ECommerce.Web.Models.Home;

public class HomeProductsListModel
{
    public IEnumerable<Product> LatestProducts { get; set; }
    public IEnumerable<Product> PopularProducts { get; set; }
    public IEnumerable<Product> TrendingProducts { get; set; }
}
