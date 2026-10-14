

using FluentValidation;

namespace Core.Application.Features.UserPhones.Queries.GetUserPhones
{
    public class GetUserPhonesQueryValidator : AbstractValidator<GetUserPhonesQuery>
    {
        public GetUserPhonesQueryValidator()
        {
            RuleFor(up => up.UserId)
                .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
