using ECommerce.Core.Entities.Base;
using ECommerce.Core.Enums;

namespace ECommerce.Core.Entities;

public class Category : AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public MainCategory MainCategory { get; set; }
}
