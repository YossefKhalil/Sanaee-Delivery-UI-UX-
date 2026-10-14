

using FluentValidation;

namespace Core.Application.Features.Users.Commands.DeActivateUser
{
    public class DeActivateUserCommandValidaor : AbstractValidator<DeActivateUserCommand>
    {
        public DeActivateUserCommandValidaor()
        {
            RuleFor(u => u.UserId)
                .NotEmpty().WithMessage("User Id is required!");
        }
    }
}
