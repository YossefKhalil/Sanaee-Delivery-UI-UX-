
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Addresses.Commands.SetDefaultAddress
{
    public class SetDefaultAddressCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<SetDefaultAddressCommand>
    {
        public async Task Handle(SetDefaultAddressCommand request, CancellationToken cts)
        {
            // 1️⃣ جلب العنوان المطلوب تعيينه كافتراضي
            var targetAddress = await unitOfWork.AddressRepository.GetByIdAsync(request.AddressId, cts);

            // 2️⃣ التحقق من وجود العنوان وأنه ينتمي بالفعل لهذا المستخدم (الأمان والحماية)
            if (targetAddress is null || targetAddress.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Address), request.AddressId);
            }

            // 3️⃣ تحسين أداء: إذا كان العنوان محدداً كافتراضي بالفعل، لا داعي لإجراء استعلامات إضافية
            if (targetAddress.IsDefault)
            {
                return;
            }

            // 4️⃣ جلب العنوان الافتراضي الحالي للمستخدم (إن وجد)
            var currentDefaultAddress = await unitOfWork.AddressRepository.GetDefaultAddressAsync(request.UserId, cts);

            // 5️⃣ إذا كان هناك عنوان افتراضي سابق، قم بتعطيله (IsDefault = false)
            if (currentDefaultAddress is not null)
            {
                currentDefaultAddress.IsDefault = false;
                unitOfWork.AddressRepository.Update(currentDefaultAddress);
            }

            // 6️⃣ جعل العنوان الجديد هو العنوان الافتراضي (IsDefault = true)
            targetAddress.IsDefault = true;
            unitOfWork.AddressRepository.Update(targetAddress);

            // 7️⃣ تنفيذ وحفظ التغييرين معاً في قاعدة البيانات كـ Transaction واحدة
            await unitOfWork.CompleteAsync(cts);
        }
    }
}