namespace ECommerce.Web.Helpers.RequestModels;

public class ProductFilterRequestModel
{
    public int SortValue { get; set; } = 0;
    public string? SearchString { get; set; } = null;
    public int PageSize { get; set; } = 12;
    public int CurrentPage { get; set; } = 0;
}
