using ECommerce.Core.Entities.Base;

namespace ECommerce.Core.Entities;

public class SubCategory : AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}
