using MediatR;

namespace Core.Application.Features.Auth.Commands.ChangePassword
{
    public sealed record ChangePasswordCommand(int UserId, string NewPassword) : IRequest;
}
