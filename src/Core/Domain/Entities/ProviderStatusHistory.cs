using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class ProviderStatusHistory
    {
        public int HistoryId { get; set; }
        public ProviderStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime ChangedAt { get; set; }


        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = null!;


        public int ChangedBy { get; set; }
        public User Admin { get; set; } = null!;

    }
}
