using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models.Account;

public class RegisterModel
{
    [Required, EmailAddress, Display(Name = "Email"), MaxLength(25, ErrorMessage = "Email can't exceed 25 characters.")]
    public string Email { get; set; }

    [Required, DataType(DataType.Password), Display(Name = "Password")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string Password { get; set; }

    [DataType(DataType.Password), Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string ReturnUrl { get; set; }
}
