

using Core.Application.Features.UserPhones.Dto;
using MediatR;

namespace Core.Application.Features.UserPhones.Queries.GetUserPhones
{
    public sealed record GetUserPhonesQuery(int UserId) : IRequest<IReadOnlyList<GetUserPhonesDto>>;
    
}
