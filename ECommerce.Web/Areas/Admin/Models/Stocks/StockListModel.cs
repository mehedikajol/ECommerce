using ECommerce.Application.BusinessEntities;

namespace ECommerce.Web.Areas.Admin.Models.Stocks;

public class StockListModel
{
    public IEnumerable<Stock> Stocks { get; set; }
    /*
    public Guid Id { get; set; }
    public int StockAmout { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    */
}
