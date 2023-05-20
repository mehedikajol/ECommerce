using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models.Profile;

public class ProfileEditModel
{
    public Guid Id { get; set; }
    [Required, Display(Name = "First Name"), MaxLength(50, ErrorMessage = "First name can't exceed 50 characters.")]
    public string FirstName { get; set; }

    [Required, Display(Name ="Last Name"), MaxLength(50, ErrorMessage = "Last name can't exceed 50 characters.")]
    public string LastName { get; set; }
    public string? ProfilePictureUrl { get; set; }

    [Required, MaxLength(50, ErrorMessage = "Email can't exceed 50 characters.")]
    public string Email { get; set; }

    [MaxLength(50, ErrorMessage = "Phone number can't exceed 50 characters.")]
    public string Phone { get; set; }

    public int Gender { get; set; }


    [Required, MaxLength(250, ErrorMessage = "Address can't exceed 50 characters.")]
    public string Address { get; set; }

    public IFormFile? ImageFile { get; set; }
}
