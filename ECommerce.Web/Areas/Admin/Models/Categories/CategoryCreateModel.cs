using Autofac;
using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Areas.Admin.Models.Categories
{
    public class CategoryCreateModel
    {
        private ICategoryService _categoryService;
        private ILifetimeScope _scope;

        public CategoryCreateModel()
        {
            
        }

        public CategoryCreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _categoryService = _scope.Resolve<ICategoryService>();
        }

        [Required, MaxLength(50, ErrorMessage = "Name can't be more than 50 characters!")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Description can't be more than 250 characters!")]
        public string Description { get; set; }

        [Required]
        public int MainCategoryId { get; set; }

        public async Task Create()
        {
            var category = new Category
            {
                Name = Name,
                Description = Description,
                MainCategoryId = MainCategoryId
            };
            await _categoryService.CreateCategory(category);
        }
    }
}
