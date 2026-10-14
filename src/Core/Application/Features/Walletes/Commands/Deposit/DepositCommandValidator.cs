

using FluentValidation;

namespace Core.Application.Features.Walletes.Commands.Deposit
{
    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            RuleFor(w => w.dto.WalletId)
                .NotEmpty().WithMessage("Wallet Id is required!");

            RuleFor(w => w.dto.Amount)
                .NotEmpty().WithMessage("Amount is required!");

            RuleFor(w => w.UserID)
                .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
