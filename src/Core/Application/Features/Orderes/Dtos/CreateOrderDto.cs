

namespace Core.Application.Features.Orderes.Dtos
{
    public class CreateOrderDto
    {
        public DateTime? VisitingTime { get; set; }
        public string? ProblemPhoto { get; set; }
        public string ProblemDescription { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int AddressId { get; set; }
    }
}
