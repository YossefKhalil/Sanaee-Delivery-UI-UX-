using FluentValidation;

namespace Core.Application.Features.WalletTransactiones.Commands.CreateWalletTransaction
{
    public class CreateWalletTransactionCommandValidator : AbstractValidator<CreateWalletTransactionCommand>
    {
        public CreateWalletTransactionCommandValidator()
        {
            RuleFor(wt => wt.Amount)
                .NotEmpty().WithMessage("Amount is required!");

            RuleFor(wt => wt.Type)
                .IsInEnum().WithMessage("Invalid Transaction Type")
                .NotEmpty().WithMessage("Amount is required!");

            RuleFor(wt => wt.Description)
                .NotEmpty().WithMessage("Description is required!");

            RuleFor(wt => wt.WalletId)
                .NotEmpty().WithMessage("Wallet Id is required!");
        }
    }
}
