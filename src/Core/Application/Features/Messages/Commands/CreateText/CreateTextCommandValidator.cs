using FluentValidation;

namespace Core.Application.Features.Messages.Commands.CreateText
{
    public class CreateTextCommandValidator : AbstractValidator<CreateTextCommand>
    {
        public CreateTextCommandValidator()
        {
            RuleFor(m => m.SenderId)
                .NotEmpty().WithMessage("Sender Id is required!");

            RuleFor(m => m.dto.RecipientId)
                .NotEmpty().WithMessage("Recipient Id is required!");

            RuleFor(m => m.dto.Content)
                .NotEmpty().WithMessage("Message Type is required!")
                .MaximumLength(1000).WithMessage("Text Exceeded the  limit!");

            RuleFor(m => m.dto.OrderId)
                .NotEmpty().WithMessage("Order Id is required!");

        }
    }
}
