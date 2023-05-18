namespace ECommerce.Web.Models.Cart;

public class CartProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}
