

using MediatR;

namespace Core.Application.Features.Addresses.Commands.CreateAddress
{
    public sealed record CreateAddressCommand(
        string Title,
        string City,
        string Region,
        string Street,
        string? BuildingNumber,
        string? ApartmentNumber,
        string? Notes,
        double? Latitude,
        double? Longitude,
        bool IsDefault,
        int UserId
    ) : IRequest<int>;
    
}
