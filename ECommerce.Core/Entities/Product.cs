using ECommerce.Core.Entities.Base;

namespace ECommerce.Core.Entities;

public class Product : AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string SKU { get; set; }
    public decimal Price { get; set; }

    public Guid SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
}
