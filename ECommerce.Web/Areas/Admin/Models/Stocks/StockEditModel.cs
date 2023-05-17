using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Areas.Admin.Models.Stocks;

public class StockEditModel
{
    public Guid Id { get; set; }
    [Display(Name = "Current Stock")]
    public int StockAmout { get; set; }
    public Guid ProductId { get; set; }

    [Display(Name = "Add to stock"), Range(0, 5000, ErrorMessage ="You can add between 0 to 5000 product to stock at a time")]
    public int AddStock { get; set; } = 0;
    [Display(Name = "Remove from stock"), Range(0, 5000, ErrorMessage = "You can remove between 0 to 5000 product from stock at a time")]
    public int RemoveStock { get; set; } = 0;
}
