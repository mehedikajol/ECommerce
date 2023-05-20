using ECommerce.Core.Enums;

namespace ECommerce.Web.Models.Profile;

public class ProfileDetailsModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
}
