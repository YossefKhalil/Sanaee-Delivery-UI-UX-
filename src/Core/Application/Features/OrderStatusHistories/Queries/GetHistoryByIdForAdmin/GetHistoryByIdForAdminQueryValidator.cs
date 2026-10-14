

using FluentValidation;

namespace Core.Application.Features.OrderStatusHistories.Queries.GetHistoryByIdForAdmin
{
    public class GetHistoryByIdForAdminQueryValidator : AbstractValidator<GetHistoryByIdForAdminQuery>
    {
        public GetHistoryByIdForAdminQueryValidator()
        {
            RuleFor(osh => osh.HistoryId)
                .NotEmpty().WithMessage("History Id is required!");
        }
    }
}
