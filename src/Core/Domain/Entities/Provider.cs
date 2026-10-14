
namespace Core.Domain.Entities
{
    public class Provider
    {
        public int ProviderId { get; set; }
        public string NationalNumber { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }


        public int UserId { get; set; }
        public User User { get; set; } = null!;


        public ICollection<ApplicationHistory> ApplicationHistories { get; set; } = new List<ApplicationHistory>();
        public ICollection<ProviderDocument> ProviderDocuments { get; set; } = new List<ProviderDocument>();
        public ICollection<ProviderCategory> ProviderCategories { get; set; } = new List<ProviderCategory>();

    }
}
