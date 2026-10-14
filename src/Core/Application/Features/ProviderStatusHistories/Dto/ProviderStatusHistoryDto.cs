using Core.Domain.Enums;

namespace Core.Application.Features.ProviderStatusHistories.Dto
{
    public class ProviderStatusHistoryDto
    {
        public ProviderStatus Status { get; set; }
        public string? Notes { get; set; }
        public int ProviderId { get; set; }

    }
}
