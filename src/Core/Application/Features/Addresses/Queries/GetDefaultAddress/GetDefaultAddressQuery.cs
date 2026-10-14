

using Core.Application.Features.Addresses.Dtos;
using MediatR;

namespace Core.Application.Features.Addresses.Queries.GetDefaultAddress
{
    public sealed record GetDefaultAddressQuery(int UserId ) : IRequest<AddressDto>;
    
}
