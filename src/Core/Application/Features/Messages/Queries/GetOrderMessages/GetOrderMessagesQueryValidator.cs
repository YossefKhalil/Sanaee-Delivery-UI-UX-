using FluentValidation;

namespace Core.Application.Features.Messages.Queries.GetOrderMessages
{
    public class GetOrderMessagesQueryValidator : AbstractValidator<GetOrderMessagesQuery>
    {
        public GetOrderMessagesQueryValidator()
        {
            RuleFor(m => m.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");
        }
    }
}
