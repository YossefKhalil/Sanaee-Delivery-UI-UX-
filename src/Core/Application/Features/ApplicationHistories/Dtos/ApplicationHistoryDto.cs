
using Core.Domain.Enums;

namespace Core.Application.Features.ApplicationHistories.Dtos
{
    public class ApplicationHistoryDto
    {
        public ApplicationStatus Status { get; set; }
        public string? Notes { get; set; }
        public int ProviderId { get; set; }
    }
}
