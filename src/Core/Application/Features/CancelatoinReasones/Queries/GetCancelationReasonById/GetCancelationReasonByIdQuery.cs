

using Application.Features.CancelatoinReasones.Dto;
using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Queries.GetCancelationReasonById
{
    public sealed record GetCancelationReasonByIdQuery(int ReasonId) : IRequest<GetCancelationReasonByIdDto>;
}
