


using FluentValidation;

namespace Core.Application.Features.Users.Commands.ActivateUser
{
    public class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
    {
        public ActivateUserCommandValidator()
        {
            RuleFor(u => u.UserId)
                .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
