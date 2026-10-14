

using MediatR;

namespace Core.Application.Features.Orderes.Commands.CancelOrderByAdmin
{
    public sealed record CancelOrderByAdminCommand(int OrderId, int ReasonId, string? Notes, bool IssueRefund) : IRequest;
}
