using FluentValidation;

namespace Core.Application.Features.Pymentes.Commands.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(p => p.Amount)
                .NotEmpty().WithMessage("Amount is required!");

            RuleFor(p => p.Method)
                .IsInEnum().WithMessage("Invalid Method!")
                .NotEmpty().WithMessage("Method is required!");

            RuleFor(p => p.Type)
                .IsInEnum().WithMessage("Invalid Type!")
                .NotEmpty().WithMessage("Type is required!");

            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
