namespace ECommerce.Web.Models.Profile;

public class ProfileIndexModel
{
    public int TotalOrders { get; set; }
    public decimal TotalSpend { get; set; }
    public int TotalProducts { get; set; }
    public int PendingOrders { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string Motto { get; set; } = string.Empty;
}
