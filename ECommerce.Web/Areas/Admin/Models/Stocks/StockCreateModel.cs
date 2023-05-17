using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Areas.Admin.Models.Stocks;

public class StockCreateModel
{
    [Display(Name="Stock"), Range(0, 5000, ErrorMessage = "You can add between 0 to 5000 product to stock at a time")]
    public int StockAmout { get; set; }
    public Guid ProductId { get; set; }
}
