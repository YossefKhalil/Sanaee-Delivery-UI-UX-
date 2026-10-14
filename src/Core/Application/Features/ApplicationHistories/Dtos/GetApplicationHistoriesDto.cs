

using Core.Domain.Enums;

namespace Core.Application.Features.ApplicationHistories.Dtos
{
    public class GetApplicationHistoriesDto
    {
        // Applicatoin History Data
        public int HistoryId { get; set; }
        public ApplicationStatus Status { get; set; }
        public string? Notes { get; set; }
        public DateTime ChangedAt { get; set; }


        // Admin Data
        public int ChangedBy { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;


        public int ProviderId { get; set; }
    }
}
