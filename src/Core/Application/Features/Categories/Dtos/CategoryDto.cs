

namespace Core.Application.Features.Categories.Dtos
{
    public class CategoryDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
