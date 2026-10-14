
using Core.Domain.Enums;

namespace Application.Features.CancelatoinReasones.Dto
{
    public class GetCancelationReasonByIdDto
    {
        public int ReasonId { get; }
        public string Text { get; } = string.Empty;
        public UserType ApplicableTo { get; }
    }
}
