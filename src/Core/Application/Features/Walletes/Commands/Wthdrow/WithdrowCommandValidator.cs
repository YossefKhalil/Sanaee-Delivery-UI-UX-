

using FluentValidation;

namespace Core.Application.Features.Walletes.Commands.Wthdrow
{
    public class WithdrowCommandValidator : AbstractValidator<WithdrowCommand>
    {
        public WithdrowCommandValidator()
        {
            RuleFor(w => w.dto.WalletId)
               .NotEmpty().WithMessage("Wallet Id is required!");

            RuleFor(w => w.dto.Amount)
                .NotEmpty().WithMessage("Amount is required!");

            RuleFor(w => w.UserId)
                .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
