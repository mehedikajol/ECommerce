using ECommerce.Core.Entities.Base;
using ECommerce.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Core.Entities;

public class UserProfile : AuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePictureUrl { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }

    public IdentityUser User { get; set; }
}
