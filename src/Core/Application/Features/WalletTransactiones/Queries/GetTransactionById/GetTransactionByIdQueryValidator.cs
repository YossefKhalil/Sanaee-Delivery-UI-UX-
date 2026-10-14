using FluentValidation;

namespace Core.Application.Features.WalletTransactiones.Queries.GetTransactionById
{
    public class GetTransactionByIdQueryValidator : AbstractValidator<GetTransactionByIdQuery>
    {
        public GetTransactionByIdQueryValidator()
        {
            RuleFor(wt => wt.TransactionId)
                .NotEmpty().WithMessage("Transaction Id is required!");
        }
    }
}
