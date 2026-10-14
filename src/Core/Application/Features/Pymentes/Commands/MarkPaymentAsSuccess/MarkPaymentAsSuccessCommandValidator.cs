using FluentValidation;

namespace Core.Application.Features.Pymentes.Commands.MarkPaymentAsSuccess
{
    public class MarkPaymentAsSuccessCommandValidator : AbstractValidator<MarkPaymentAsSuccessCommand>
    {
        public MarkPaymentAsSuccessCommandValidator()
        {
            RuleFor(p => p.PaymentId)
                .NotEmpty().WithMessage("Payment Id is required!");

            RuleFor(p => p.TransactionReference)
                .NotEmpty().WithMessage("Transaction Reference Id is required!");
        }
    }
}
