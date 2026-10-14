

using MediatR;

namespace Core.Application.Features.UserPhones.Commands.VerifyPhoneOtp
{
    public sealed record VerifyPhoneOtpCommand(int UserId, string PhoneNumber, string Code) : IRequest;
    
}
