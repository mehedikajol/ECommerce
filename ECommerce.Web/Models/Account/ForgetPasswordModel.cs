using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models.Account;

public class ForgetPasswordModel
{
    [Required, EmailAddress, Display(Name = "Email"), MaxLength(25, ErrorMessage ="Email can't exceed 25 characters.")]
    public string Email { get; set; }
}
