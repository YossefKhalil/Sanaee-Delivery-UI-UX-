using Core.Application.Features.Users.Dtos;
using MediatR;

namespace Core.Application.Features.Users.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(

        int UserId,
        UpdateUserDto User

        ) : IRequest;
    
}

