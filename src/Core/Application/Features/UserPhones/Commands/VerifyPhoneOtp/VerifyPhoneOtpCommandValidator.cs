

using FluentValidation;

namespace Core.Application.Features.UserPhones.Commands.VerifyPhoneOtp
{
    public class VerifyPhoneOtpCommandValidator : AbstractValidator<VerifyPhoneOtpCommand>
    {
        public VerifyPhoneOtpCommandValidator()
        {
            RuleFor(up => up.UserId)
                .NotEmpty().WithMessage("User Id is required!");

            RuleFor(up => up.PhoneNumber)
                .NotEmpty().WithMessage("Phone Numberd is required!");

            RuleFor(up => up.Code)
                .NotEmpty().WithMessage("The Code is required!");
        }
    }
}
