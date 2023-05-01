namespace ECommerce.Application.BusinessEntities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int MainCategory { get; set; }
        public string MainCategoryName { get; set; } = string.Empty;
    }
}
