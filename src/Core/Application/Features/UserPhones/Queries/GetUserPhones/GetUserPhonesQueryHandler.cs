
using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.UserPhones.Dto;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.UserPhones.Queries.GetUserPhones
{
    public sealed class GetUserPhonesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUserPhonesQuery, IReadOnlyList<GetUserPhonesDto>>
    {
        async Task<IReadOnlyList<GetUserPhonesDto>> IRequestHandler<GetUserPhonesQuery, IReadOnlyList<GetUserPhonesDto>>.Handle(GetUserPhonesQuery request, CancellationToken cts)
        {
            var phones = await unitOfWork.UserPhoneRepository.GetAllByUserIdAsync(request.UserId, cts);

            if (phones.Count == 0)
                throw new NotFoundException(nameof(UserPhone), request.UserId);

            return mapper.Map<IReadOnlyList<GetUserPhonesDto>>(phones);
        }
    }
}
