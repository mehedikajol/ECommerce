using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Areas.Admin.Models.Products;

public class ProductEditModel
{
    public Guid Id { get; set; }
    [Required, MaxLength(250, ErrorMessage = "Name can't be more than 250 characters!")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000, ErrorMessage = "Description can't be more than 1000 characters!")]
    public string Description { get; set; } = string.Empty;
    //public string SKU { get; set; }
    //public string ImageUrl { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public Guid Category { get; set; }
}
