
using FluentValidation;

namespace Core.Application.Features.UserPhones.Commands.DeleteUserPhone
{
    public class DeleteUserPhoneCommandValidator : AbstractValidator<DeleteUserPhoneCommand>
    {
        public DeleteUserPhoneCommandValidator()
        {
            RuleFor(up => up.UserId)
                .NotEmpty().WithMessage("User Id is required");

            RuleFor(up => up.PhoneId)
                .NotEmpty().WithMessage("Phone Id is required");
        }
    }
}
