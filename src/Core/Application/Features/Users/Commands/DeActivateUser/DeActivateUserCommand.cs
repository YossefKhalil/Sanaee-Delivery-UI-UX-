

using MediatR;

namespace Core.Application.Features.Users.Commands.DeActivateUser
{
    public sealed record DeActivateUserCommand(int UserId) : IRequest;
    
    
}
