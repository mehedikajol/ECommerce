namespace ECommerce.Application.BusinessEntities;

public class Stock
{
    public Guid Id { get; set; }
    public int StockAmout { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
} 
