using Core.Application.Features.Addresses.Dtos;
using MediatR;

namespace Core.Application.Features.Addresses.Queries.GetAddressById
{
    public sealed record GetAddressByIdQuery(int AddressId) : IRequest<AddressDto>;
}
