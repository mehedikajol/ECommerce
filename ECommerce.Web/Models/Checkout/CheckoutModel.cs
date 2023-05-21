namespace ECommerce.Web.Models.Checkout;

public class CheckoutModel
{
    public List<CheckoutProductModel> Products { get; set; }
    public CheckoutUserModel User { get; set; }
    public int PaymentMethod { get; set; }
}
