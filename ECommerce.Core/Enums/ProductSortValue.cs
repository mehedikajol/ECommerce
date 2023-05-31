using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Enums;

// This enum is used to sort products
public enum ProductSortValue
{
    [Display(Name = "Name")]
    Name = 1,

    [Display(Name = "Price low to high")]
    PriceLowToHigh,

    [Display(Name = "Price high to low")]
    PriceHighToLow,

    [Display(Name = "Popularity")]
    Popularity,

    [Display(Name = "Newest")]
    Newest
}
