

using MediatR;

namespace Core.Application.Features.Addresses.Commands.DeleteAddress
{
    public sealed record DeleteAddressCommand( int AddressId ) : IRequest;
    
}
