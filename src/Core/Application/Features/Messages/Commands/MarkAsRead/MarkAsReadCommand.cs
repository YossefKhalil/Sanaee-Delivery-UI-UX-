using MediatR;

namespace Core.Application.Features.Messages.Commands.MarkAsRead
{
    public sealed record MarkAsReadCommand(int SenderId, int OrderId) : IRequest;
}
