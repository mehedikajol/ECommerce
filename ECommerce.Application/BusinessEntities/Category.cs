namespace ECommerce.Application.BusinessEntities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int MainCategoryId { get; set; }
        public string MainCategory { get; set; } = string.Empty;
    }
}
