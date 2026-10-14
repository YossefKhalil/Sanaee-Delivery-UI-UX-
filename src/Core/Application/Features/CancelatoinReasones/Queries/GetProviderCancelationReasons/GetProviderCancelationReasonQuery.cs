using Core.Application.Features.CancelatoinReasones.Dto;
using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Queries.GetProviderCancelationReasons
{
    public sealed record GetProviderCancelationReasonQuery : IRequest<IReadOnlyList<GetUserCancelationReasonDto>>;
}
