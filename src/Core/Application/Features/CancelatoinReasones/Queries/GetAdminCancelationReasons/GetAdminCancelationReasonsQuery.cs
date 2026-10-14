using Core.Application.Features.CancelatoinReasones.Dto;
using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Queries.GetAdminCancelationReasons
{
    public sealed record GetAdminCancelationReasonsQuery : IRequest<IReadOnlyList<GetUserCancelationReasonDto>>;
}
