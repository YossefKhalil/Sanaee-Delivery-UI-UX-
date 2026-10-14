

using FluentValidation;

namespace Core.Application.Features.Orderes.Queries.GetOrderById
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(o => o.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");
        }
    }
}
