using ECommerce.Core.Entities.Base;

namespace ECommerce.Core.Entities;

public class Stock : AuditableEntity
{
    public int StockAmout { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
