using FluentValidation;

namespace Core.Application.Features.Pymentes.Commands.RefundPayment
{
    public class RefundPaymentCommandValidator : AbstractValidator<RefundPaymentCommand>
    {
        public RefundPaymentCommandValidator()
        {
            RuleFor(p => p.PaymentId)
                .NotEmpty().WithMessage("Payment Id is required!");
        }
    }
}
