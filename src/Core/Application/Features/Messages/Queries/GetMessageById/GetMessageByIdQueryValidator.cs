using FluentValidation;

namespace Core.Application.Features.Messages.Queries.GetMessageById
{
    public class GetMessageByIdQueryValidator : AbstractValidator<GetMessageByIdQuery>
    {
        public GetMessageByIdQueryValidator()
        {
            RuleFor(m => m.MessageId)
                .NotEmpty().WithMessage("Message Id is required!");
        }
    }
}
