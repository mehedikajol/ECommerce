using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models.Account;

public class SignInModel
{
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }

    public string ReturnUrl { get; set; }
}
