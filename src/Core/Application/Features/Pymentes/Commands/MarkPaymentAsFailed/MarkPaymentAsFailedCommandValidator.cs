using FluentValidation;

namespace Core.Application.Features.Pymentes.Commands.MarkPaymentAsFailed
{
    public class MarkPaymentAsFailedCommandValidator : AbstractValidator<MarkPaymentAsFailedCommand>
    {
        public MarkPaymentAsFailedCommandValidator()
        {
            RuleFor(p => p.PaymentId)
                .NotEmpty().WithMessage("Payment Id is required!");
        }
    }
}
