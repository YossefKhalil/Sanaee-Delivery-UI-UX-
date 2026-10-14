namespace Core.Application.Features.Reviewes.Dto
{
    public class CreateReviewDto
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int RevieweeId { get; set; }
        public int OrderId { get; set; }
    }
}
