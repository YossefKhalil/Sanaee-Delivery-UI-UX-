

using Core.Application.Features.UserPhones.Dto;
using MediatR;

namespace Core.Application.Features.UserPhones.Queries.GetDefaultPhone
{
    public sealed record GetDefaultPhoneQuery(int UserId) : IRequest<GetUserPhonesDto>;
    
}
