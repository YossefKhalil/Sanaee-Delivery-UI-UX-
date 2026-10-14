

using Core.Application.Features.Addresses.Dtos;
using MediatR;

namespace Core.Application.Features.Addresses.Commands.UpdateAddress
{
    public sealed record UpdateAddressCommand(
        int AddressId,
        CreateUadateAddressDto address
    ) : IRequest;
    
}
