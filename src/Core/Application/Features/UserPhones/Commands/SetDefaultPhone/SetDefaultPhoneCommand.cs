
using MediatR;

namespace Core.Application.Features.UserPhones.Commands.SetDefaultPhone
{
    public sealed record SetDefaultPhoneCommand(int PhoneId, int UserId) : IRequest;
    
}
