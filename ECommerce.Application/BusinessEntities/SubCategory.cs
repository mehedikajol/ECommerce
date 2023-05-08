namespace ECommerce.Application.BusinessEntities;

public class SubCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string MainCategoryName { get; set; } = string.Empty;
}
