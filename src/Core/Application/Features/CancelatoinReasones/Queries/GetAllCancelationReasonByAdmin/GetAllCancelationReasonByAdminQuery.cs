using Core.Domain.Entities;
using MediatR;


namespace Core.Application.Features.CancelatoinReasones.Queries.GetAllCancelationReasonByAdmin
{
    public sealed record GetAllCancelationReasonByAdminQuery : IRequest<IReadOnlyList<CancelationReason>>;
}
