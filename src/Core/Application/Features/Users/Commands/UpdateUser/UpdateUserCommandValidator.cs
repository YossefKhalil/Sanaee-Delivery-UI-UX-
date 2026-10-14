using FluentValidation;

namespace Core.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(u => u.User.FirstName)
                .MaximumLength(50).WithMessage("First Name must not exceed 50 characters.");

            RuleFor(u => u.User.LastName)
                .MaximumLength(50).WithMessage("Last Name must not exceed 50 characters.");

            RuleFor(u => u.User.Email)
                .MaximumLength(150).WithMessage("Email must not exceed 150 characters.");
        }
    }
}
