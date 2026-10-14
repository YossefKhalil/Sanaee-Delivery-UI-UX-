

using Core.Application.Common.Contracts.Persistence;
using Microsoft.Extensions.Caching.Memory;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using AutoMapper;
using MediatR;

namespace Core.Application.Features.Provideres.Commands.CreateProvider
{
    public sealed class CreateProviderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache) : IRequestHandler<CreateProviderCommand, int>
    {
        public async Task<int> Handle(CreateProviderCommand request, CancellationToken cts)
        {

            var cacheKey = $"OTP:{request.PhoneNumber}";
            if (!cache.TryGetValue(cacheKey, out string? cachedOtp) || cachedOtp != request.OtpCode)
            {
                throw new BadRequestException("Invalid Code!");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Type = UserType.Provider,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                AverageRating = 0,
                TotalReviewCount = 0
            };

            var Phone = new UserPhone
            {
                PhoneNumber = request.PhoneNumber,
                UserId = user.UserId,
                IsDefault = true

            };

            user.Phones.Add(Phone);

            if (request.Address is not null)
            {
                var addressEntity = mapper.Map<Address>(request.Address);

                addressEntity.IsDefault = true;

                user.Addresses.Add(addressEntity);
            }



            var provider = new Provider
            {
                NationalNumber = request.NationalNumber,
                IsAvailable = false,
                User = user,
            };


            if (request.ProviderCategories is not null && request.ProviderCategories.Count != 0)
            {
                var categories = mapper.Map<IReadOnlyList<ProviderCategory>>(request.ProviderCategories);

                foreach (var category in categories)
                {
                    provider.ProviderCategories.Add(category);
                }
            }

            if(request.ProviderDocuments is not null && request.ProviderDocuments.Count != 0)
            {
                var documents = mapper.Map<IReadOnlyList<ProviderDocument>>(request.ProviderDocuments);

                foreach(var document in documents)
                {
                    document.Status = DocumentStatus.Pending;
                    provider.ProviderDocuments.Add(document);
                }
            }

            var history = new ApplicationHistory
            {
                Status = ApplicationStatus.Pending,
                Notes = "Reviewing the account is in progress.",
                ChangedAt = DateTime.UtcNow,
                User = user
            };

            provider.ApplicationHistories.Add(history);

            await unitOfWork.Repository<Provider>().AddAsync(provider, cts);

            await unitOfWork.CompleteAsync(cts);

            cache.Remove(cacheKey);

            return provider.ProviderId;
        }
    }
}
