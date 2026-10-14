

using MediatR;

namespace Core.Application.Features.Addresses.Commands.SetDefaultAddress
{
    public sealed record SetDefaultAddressCommand(int AddressId, int UserId ) : IRequest;
    
}
