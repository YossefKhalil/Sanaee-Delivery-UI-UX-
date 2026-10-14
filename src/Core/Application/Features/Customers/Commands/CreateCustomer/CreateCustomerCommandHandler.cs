using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;


namespace Core.Application.Features.Customers.Commands.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache) : IRequestHandler<CreateCustomerCommand, int>
    {
        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cts)
        {

            var cacheKey = $"OTP:{request.PhoneNumber}";
            if (!cache.TryGetValue(cacheKey, out string? cachedOtp) || cachedOtp != request.OtpCode)
            {
                throw new BadRequestException("Invalid Code!");
            }

            var User = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Type = UserType.Customer,
                IsDeleted = false,
                DeletedAt = null,
                CreatedAt = DateTime.UtcNow,
                AverageRating = 0,
                TotalReviewCount = 0
            };

            await unitOfWork.Repository<User>().AddAsync(User, cts);

            var Phone = new UserPhone
            {
                PhoneNumber = request.PhoneNumber,
                UserId = User.UserId,
                IsDefault = true
                
            };

            User.Phones.Add(Phone);

            if (request.Address is not null)
            {
                var addressEntity = mapper.Map<Address>(request.Address);

                addressEntity.IsDefault = true;

                User.Addresses.Add(addressEntity);
            }

            await unitOfWork.CompleteAsync(cts);

            cache.Remove(cacheKey);

            return User.UserId;
        }
    }
}