
using Core.Domain.Enums;

namespace Core.Domain.Entities
{
    public class ApplicationHistory
    {
        public int HistoryId { get; set; }
        public ApplicationStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime ChangedAt { get; set; }


        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = null!;


        public int ChangedBy { get; set; }
        public User User { get; set; } = null!;
    }
}
