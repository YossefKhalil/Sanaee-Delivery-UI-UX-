
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Addresses.Commands.UpdateAddress
{
    public sealed class UpdateAddressCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateAddressCommand>
    {
        public async Task Handle(UpdateAddressCommand request, CancellationToken cts)
        {
            // 1. جلب العنوان المراد تعديله من قاعدة البيانات
            var address = await unitOfWork.Repository<Address>().GetByIdAsync(request.AddressId, cts);

            // 2. التحقق من وجود العنصر
            if (address == null)
            {
                throw new NotFoundException(nameof(Address), request.AddressId);
            }

            // 3. تحديث البيانات


            if (!string.IsNullOrWhiteSpace(request.address.Title))
                address.Title = request.address.Title;

            if (!string.IsNullOrWhiteSpace(request.address.City))
                address.City = request.address.City;

            if (!string.IsNullOrWhiteSpace(request.address.Region))
                address.Region = request.address.Region;

            if (!string.IsNullOrWhiteSpace(request.address.Street))
                address.Street = request.address.Street;

            if (!string.IsNullOrWhiteSpace(request.address.BuildingNumber))
                address.BuildingNumber = request.address.BuildingNumber;

            if (!string.IsNullOrWhiteSpace(request.address.ApartmentNumber))
                address.ApartmentNumber = request.address.ApartmentNumber;

            if (!string.IsNullOrWhiteSpace(request.address.Notes))
                address.Notes = request.address.Notes;

            address.Latitude = request.address.Latitude ?? address.Latitude;
            address.Longitude = request.address.Longitude ?? address.Longitude;
            address.IsDefault = request.address.IsDefault ?? address.IsDefault;

            // 4. تطبيق التعديل والحفظ عبر الـ UnitOfWork
            unitOfWork.Repository<Address>().Update(address);
            await unitOfWork.CompleteAsync(cts);
        }
    }
}