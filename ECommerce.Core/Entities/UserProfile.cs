using ECommerce.Core.Entities.Base;
using ECommerce.Core.Enums;

namespace ECommerce.Core.Entities;

public class UserProfile : AuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePictureUrl { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }

    public Guid UserId { get; set; }
}
