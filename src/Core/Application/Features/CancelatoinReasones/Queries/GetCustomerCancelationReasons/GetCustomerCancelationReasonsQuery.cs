using Core.Application.Features.CancelatoinReasones.Dto;
using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Queries.GetCustomerCancelationReasons
{
    public sealed record GetCustomerCancelationReasonsQuery : IRequest<IReadOnlyList<GetUserCancelationReasonDto>>;
}
