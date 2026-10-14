using FluentValidation;

namespace Core.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User Id is required!");

            RuleFor(w => w.IpAddress)
               .NotEmpty().WithMessage("Ip Address required!");

            RuleFor(w => w.UserAgent)
               .NotEmpty().WithMessage("User Agent is required!");
        }
    }
}
