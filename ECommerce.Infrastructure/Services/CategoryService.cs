using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;

namespace ECommerce.Infrastructure.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = new List<Category>();
            var categoryEntities = await _unitOfWork.Categories.GetAllEntities();
            
            foreach(var entity in categoryEntities)
            {
                categories.Add(new Category
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    MainCategoryId = entity.MainCategoryId
                });
            }

            return categories;
        }
    }
}
