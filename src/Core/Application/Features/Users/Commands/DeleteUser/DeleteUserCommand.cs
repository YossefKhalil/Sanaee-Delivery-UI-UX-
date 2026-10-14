using MediatR;

namespace Core.Application.Features.Users.Commands.DeleteUser
{
    public sealed record DeleteUserCommand(int UserId, string IpAddress, string UserAgent) : IRequest;
}
