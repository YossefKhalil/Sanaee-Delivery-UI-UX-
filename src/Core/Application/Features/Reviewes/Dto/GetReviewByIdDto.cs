using Core.Domain.Entities;

namespace Application.Features.Reviewes.Dto
{
    public class GetReviewByIdDto
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsEdited { get; private set; }
        public int RevieweeId { get; set; }
        public User Reviewer { get; set; } = null!;
        public int OrderId { get; set; }
    }
}
