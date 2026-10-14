
using FluentValidation;

namespace Core.Application.Features.Walletes.Commands.DeActivateWallet
{
    public class DeActivateWalletCommandValidator : AbstractValidator<DeActivateWalletCommand>
    {
        public DeActivateWalletCommandValidator()
        {
            RuleFor(w => w.WalletId)
                .NotEmpty().WithMessage("Wallet Id is required!");

            RuleFor(w => w.UserId)
                .NotEmpty().WithMessage("User Id is required!");

            RuleFor(w => w.IpAddress)
               .NotEmpty().WithMessage("User Id is required!");

            RuleFor(w => w.UserAgent)
               .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
