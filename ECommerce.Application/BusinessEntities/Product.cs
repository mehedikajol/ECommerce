namespace ECommerce.Application.BusinessEntities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Guid SubCategoryId { get; set; }
    public string SubCategoryName { get; set; } = string.Empty;
}
