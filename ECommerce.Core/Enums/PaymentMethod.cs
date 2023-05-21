using System.ComponentModel.DataAnnotations;

namespace ECommerce.Core.Enums;

public enum PaymentMethod
{
    [Display(Name = "Cash On Delivery")]
    CashOnDelivery = 1,

    [Display(Name = "Bank Transfer")]
    BankTransfer,

    [Display(Name = "Mobile Banking")]
    MobileBanking
}
