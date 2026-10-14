


using MediatR;

namespace Core.Application.Features.UserPhones.Commands.DeleteUserPhone
{
    public sealed record DeleteUserPhoneCommand(int PhoneId, int UserId) : IRequest;
    
    
}
