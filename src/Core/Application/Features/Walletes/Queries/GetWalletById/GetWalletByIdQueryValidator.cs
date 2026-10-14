

using FluentValidation;

namespace Core.Application.Features.Walletes.Queries.GetWalletById
{
    public class GetWalletByIdQueryValidator : AbstractValidator<GetWalletByIdQuery>
    {
        public GetWalletByIdQueryValidator()
        {
            RuleFor(w => w.WalletId)
                .NotEmpty().WithMessage("Wallet Id is required!");

            RuleFor(w => w.UserId)
                .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
