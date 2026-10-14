
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Addresses.Commands.DeleteAddress
{
    public sealed class DeleteAddressCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteAddressCommand>
    {
        public async Task Handle(DeleteAddressCommand request, CancellationToken cts)
        {
            // 1. جلب العنوان المراد تعديله من قاعدة البيانات
            var address = await unitOfWork.Repository<Address>().GetByIdAsync(request.AddressId, cts);

            // 2. التحقق من وجود العنصر
            if (address == null)
            {
                throw new NotFoundException(nameof(Address), request.AddressId);
            }


            // 4. تطبيق التعديل والحفظ عبر الـ UnitOfWork
            unitOfWork.Repository<Address>().Delete(address);
            await unitOfWork.CompleteAsync(cts);
        }
    }
}