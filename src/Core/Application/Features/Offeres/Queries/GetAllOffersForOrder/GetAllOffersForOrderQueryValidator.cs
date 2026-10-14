
using FluentValidation;

namespace Core.Application.Features.Offeres.Queries.GetAllOffersForOrder
{
    public class GetAllOffersForOrderQueryValidator : AbstractValidator<GetAllOffersForOrderQuery>
    {
        public GetAllOffersForOrderQueryValidator()
        {
            RuleFor(o => o.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");
        }
    }
}
