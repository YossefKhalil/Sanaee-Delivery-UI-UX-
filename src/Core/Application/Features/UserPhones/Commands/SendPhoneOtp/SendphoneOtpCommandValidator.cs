

using FluentValidation;

namespace Core.Application.Features.UserPhones.Commands.SendPhoneOtp
{
    public class SendphoneOtpCommandValidator : AbstractValidator<SendphoneOtpCommand>
    {
        public SendphoneOtpCommandValidator()
        {
            RuleFor(up => up.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone Number is wrong!");
        }
    }
}
