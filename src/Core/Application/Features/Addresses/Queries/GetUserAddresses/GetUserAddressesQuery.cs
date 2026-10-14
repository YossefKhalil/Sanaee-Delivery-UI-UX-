

using Core.Application.Features.Addresses.Dtos;
using MediatR;

namespace Core.Application.Features.Addresses.Queries.GetUserAddresses
{
    public sealed record GetUserAddressesQuery(int UserId ) : IRequest<IReadOnlyList<AddressDto>>;
    
}
