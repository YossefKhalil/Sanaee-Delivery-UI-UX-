
using FluentValidation;

namespace Core.Application.Features.UserPhones.Commands.SetDefaultPhone
{
    public class SetDefaultPhoneCommandValidator : AbstractValidator<SetDefaultPhoneCommand>
    {
        public SetDefaultPhoneCommandValidator()
        {
            RuleFor(up => up.PhoneId)
                .NotEmpty().WithMessage("Phone Id is required!");

            RuleFor(up => up.UserId)
                .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
