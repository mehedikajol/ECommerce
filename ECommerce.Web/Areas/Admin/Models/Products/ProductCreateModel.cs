using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Areas.Admin.Models.Products;

public class ProductCreateModel
{
    private IProductService _productService;
    private ISubCategoryService _subCategoryService;
    private ILifetimeScope _scope;

    public ProductCreateModel()
    {
    }

    public ProductCreateModel(
        IProductService productService,
        ISubCategoryService subCategoryService)
    {
        _productService = productService;
        _subCategoryService = subCategoryService;

    }

    internal void ResolveDependency(ILifetimeScope scope)
    {
        _scope = scope;
        _productService = _scope.Resolve<IProductService>();
        _subCategoryService = _scope.Resolve<ISubCategoryService>();
    }

    [Required, MaxLength(250, ErrorMessage = "Name can't be more than 250 characters!")]
    public string Name { get; set; }

    [MaxLength(1000, ErrorMessage = "Description can't be more than 1000 characters!")]
    public string Description { get; set; }
    //public string SKU { get; set; }
    //public string ImageUrl { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public Guid Category { get; set; }

    public async Task<IEnumerable<SubCategory>> LoadSubCategories()
    {
        return await _subCategoryService.GetAllSubCategories();
    }

    public async Task Create()
    {
        var product = new Product
        {
            Name = Name,
            Description = Description,
            ImageUrl = "https://picsum.photos/200/300", // TODO: Image should be uploaded and url saved
            SKU = "5BLP4EWUCQ59ZTD", // TODO: Random string generator needed to generate SKU
            Price = Price,
            SubCategoryId = Category
        };
        await _productService.CreateProduct(product);
    }
}
