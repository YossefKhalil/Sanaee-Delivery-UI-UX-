
using Core.Domain.Enums;

namespace Core.Application.Features.ProviderStatusHistories.Dto
{
    public class GetProviderStatusHistoriesDto
    {
        public int HistoryId { get; set; }
        public ProviderStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime ChangedAt { get; set; }
        public int ChangedBy { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int ProviderId { get; set; }
    }
}
