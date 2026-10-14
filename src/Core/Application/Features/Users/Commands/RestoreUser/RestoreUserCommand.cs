using Core.Application.Features.Users.Dtos;
using MediatR;

namespace Core.Application.Features.Users.Commands.RestoreUser
{
    public sealed record RestoreUserCommand(int UserId, CheckAcountDto dto, string IpAddress, string UserAgent) : IRequest;
    
}
