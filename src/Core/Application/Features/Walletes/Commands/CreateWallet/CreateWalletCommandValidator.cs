
using FluentValidation;

namespace Core.Application.Features.Walletes.Commands.CreateWallet
{
    public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
    {
        public CreateWalletCommandValidator()
        {
            RuleFor(w => w.UserId)
                .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
