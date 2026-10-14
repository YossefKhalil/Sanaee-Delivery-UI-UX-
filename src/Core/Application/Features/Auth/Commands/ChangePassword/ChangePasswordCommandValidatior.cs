using FluentValidation;


namespace Core.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandValidatior : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidatior()
        {
            RuleFor(u => u.UserId)
                .NotEmpty().WithMessage("Customer Id must be greater than 0!");

            RuleFor(u => u.NewPassword)
                .NotEmpty().WithMessage("New Password is required!");

        }
    }
}
