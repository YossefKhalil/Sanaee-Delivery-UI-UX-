using FluentValidation;

namespace Core.Application.Features.Messages.Commands.MarkAsRead
{
    public class MarkAsReadCommandValidator : AbstractValidator<MarkAsReadCommand>
    {
        public MarkAsReadCommandValidator()
        {
            RuleFor(m => m.SenderId)
                .NotEmpty().WithMessage("Sender Id is required!");
        }
    }
}
