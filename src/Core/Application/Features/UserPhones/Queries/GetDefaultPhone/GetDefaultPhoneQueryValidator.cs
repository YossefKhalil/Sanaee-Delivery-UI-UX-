

using FluentValidation;

namespace Core.Application.Features.UserPhones.Queries.GetDefaultPhone
{
    public class GetDefaultPhoneQueryValidator : AbstractValidator<GetDefaultPhoneQuery>
    {
        public GetDefaultPhoneQueryValidator()
        {
            RuleFor(up => up.UserId)
                .NotEmpty().WithMessage("User Id required!");
        }
    }
}
