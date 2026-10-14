

using FluentValidation;

namespace Core.Application.Features.CancelatoinReasones.Queries.GetCancelationReasonById
{
    public class GetCancelationReasonByIdQueryValidator : AbstractValidator<GetCancelationReasonByIdQuery>
    {
        public GetCancelationReasonByIdQueryValidator()
        {
            RuleFor(cr => cr.ReasonId)
                .NotEmpty().WithMessage("Reason Id is required!");
        }
    }
}
