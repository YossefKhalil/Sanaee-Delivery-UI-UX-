
using MediatR;

namespace Core.Application.Features.UserPhones.Commands.SendPhoneOtp
{
    public sealed record SendphoneOtpCommand(string PhoneNumber) : IRequest<string>;
    
}
