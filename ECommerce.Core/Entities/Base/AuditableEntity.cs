namespace ECommerce.Core.Entities.Base
{
    public class AuditableEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime InsertedDate { get; set; }
        public string InsertedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public bool ActiveStatus { get; set; }
    }
}
