namespace ECommerce.Core.Entities.Base
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
