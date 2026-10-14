
using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Addresses.Commands.CreateAddress
{
    public sealed class CreateAddressCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateAddressCommand, int>
    {
        public async Task<int> Handle(CreateAddressCommand request, CancellationToken cts)
        {
            var address = new Address
            {
                Title = request.Title,
                City = request.City,
                Region = request.Region,
                Street = request.Street,
                BuildingNumber = request.BuildingNumber,
                ApartmentNumber = request.ApartmentNumber,
                Notes = request.Notes,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                IsDefault = request.IsDefault,
                UserId = request.UserId,
                IsDeleted = false,
                DeletedAt = null
            };

            await unitOfWork.Repository<Address>().AddAsync(address, cts);
            await unitOfWork.CompleteAsync(cts);

            return address.AddressId;
        }
    }
}
