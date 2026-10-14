using Core.Application.Common.Contracts.Persistence;
using Microsoft.Extensions.Caching.Memory;
using Core.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.UserPhones.Commands.VerifyPhoneOtp
{
    public sealed class VerifyPhoneOtpCommandHandler(IMemoryCache cache, IUnitOfWork unitOfWork) : IRequestHandler<VerifyPhoneOtpCommand>
    {
        public async Task Handle(VerifyPhoneOtpCommand request, CancellationToken cts)
        {
            var cacheKey = $"OTP:{request.PhoneNumber}";

            if (!cache.TryGetValue(cacheKey, out string? cacheOtp) || cacheOtp != request.Code)
                throw new BadRequestException("Invalid Code");

            var phoneRepo = unitOfWork.Repository<UserPhone>();

            var existingPhoneInSystem = await phoneRepo
                .GetQueryable()
                .FirstOrDefaultAsync(up => up.PhoneNumber == request.PhoneNumber, cts);

            if (existingPhoneInSystem is not null && existingPhoneInSystem.UserId != request.UserId)
                phoneRepo.Delete(existingPhoneInSystem);

            var currentPhone = await phoneRepo
                .GetQueryable()
                .FirstOrDefaultAsync(up => up.UserId == request.UserId && up.PhoneNumber == request.PhoneNumber, cts);

            var hasOtpPhones = await phoneRepo
                .GetQueryable()
                .FirstOrDefaultAsync(up => up.UserId == request.UserId, cts);

            if(currentPhone is null)
            {
                var newPhone = new UserPhone
                {
                    UserId = request.UserId,
                    PhoneNumber = request.PhoneNumber,
                    //IsVerified = true,
                    //IsPrimary = !hasOtherPhones
                };
                await phoneRepo.AddAsync(newPhone, cts);
            }

            else
            {
                //currentPhone.IsVerified = true;
                //phoneRepo.Update(currentPhone);
            }

            await unitOfWork.CompleteAsync(cts);
            cache.Remove(cacheKey);
        }
    }
}
