using FluentValidation;

namespace Core.Application.Features.WalletTransactiones.Queries.GetAllTransactionsByWalletId
{
    public class GetAllWalletTransactionsByWalletIdQueryValidator : AbstractValidator<GetAllTransactionsByWalletIdQuery>
    {
        public GetAllWalletTransactionsByWalletIdQueryValidator()
        {
            RuleFor(wt => wt.WalletId)
                .NotEmpty().WithMessage("Wallet Id is required!");
        }
    }
}
