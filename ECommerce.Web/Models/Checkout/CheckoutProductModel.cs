namespace ECommerce.Web.Models.Checkout;

public class CheckoutProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}
