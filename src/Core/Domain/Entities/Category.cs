using Core.Domain.Entities.Contract;


namespace Core.Domain.Entities
{
    public class Category : ISoftDeletable
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public bool IsActive { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }


        public ICollection<Category> SubCategories { get; set; } = new List<Category>();
        public ICollection<Provider> Providers { get; set; } = new List<Provider>();


        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }



        public void Freeze()
        {
            // check if it is already Deactivated
            if (!IsActive) return;

            IsActive = false;
        }
        public void Unfreeze()
        {
            // check if it is already Activated
            if (IsActive) return;

            IsActive = true;
        }

    }
}
