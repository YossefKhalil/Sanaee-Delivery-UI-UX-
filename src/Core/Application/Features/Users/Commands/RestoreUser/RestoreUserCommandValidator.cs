using FluentValidation;

namespace Core.Application.Features.Users.Commands.RestoreUser
{
    public class RestoreUserCommandValidator : AbstractValidator<RestoreUserCommand>
    {
        public RestoreUserCommandValidator()
        {
            RuleFor(c => c.dto.Email)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(c => c.dto.Password)
                .NotEmpty().WithMessage("Password is required");

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User Id is required!");

            RuleFor(w => w.IpAddress)
               .NotEmpty().WithMessage("Ip Address Id is required!");

            RuleFor(w => w.UserAgent)
               .NotEmpty().WithMessage("User Agent Id is required!");
        }
    }
}
