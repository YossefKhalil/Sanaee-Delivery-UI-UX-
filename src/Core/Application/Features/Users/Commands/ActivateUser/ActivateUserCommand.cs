
using MediatR;

namespace Core.Application.Features.Users.Commands.ActivateUser
{
    public sealed record ActivateUserCommand(int UserId) : IRequest;
    
}
