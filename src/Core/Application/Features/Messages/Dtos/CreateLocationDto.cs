namespace Core.Application.Features.Messages.Dtos
{
    public class CreateLocationDto
    {
        public int RecipientId { get; set; }
        public int OrderId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
