using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Entities.Base;
using ECommerce.Core.Enums;
using EO = ECommerce.Core.Entities;

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

            foreach (var entity in categoryEntities)
            {
                categories.Add(new Category
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    MainCategoryId = entity.MainCategoryId,
                    MainCategory = Enum.GetName(typeof(MainCategory), entity.MainCategoryId)
                });
            }
            return categories;
        }

        public async Task CreateCategory(Category category)
        {
            var categoryEntity = new EO.Category
            {
                Name = category.Name,
                Description = category.Description,
                MainCategoryId = category.MainCategoryId
            };

            await _unitOfWork.Categories.AddEntity(categoryEntity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            var categoryEntity = await _unitOfWork.Categories.GetEntityById(id);
            if (categoryEntity is null) return null;
            var category = new Category
            {
                Id = categoryEntity.Id,
                Name = categoryEntity.Name,
                Description = categoryEntity.Description,
                MainCategoryId = categoryEntity.MainCategoryId
            };
            return category;
        }

        public async Task UpdateCategory(Category category)
        {
            var categoryEntity = await _unitOfWork.Categories.GetEntityById(category.Id);

            categoryEntity.Name = category.Name;
            categoryEntity.Description = category.Description;
            categoryEntity.MainCategoryId = category.MainCategoryId;

            await _unitOfWork.Categories.UpdateEntity(categoryEntity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCategory(Guid id)
        {
            await _unitOfWork.Categories.DeleteEntityById(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
