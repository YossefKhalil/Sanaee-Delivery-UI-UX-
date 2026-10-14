
using AutoMapper;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.UserPhones.Dto;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.UserPhones.Queries.GetDefaultPhone
{
    public sealed class GetDefaultPhoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetDefaultPhoneQuery, GetUserPhonesDto>
    {
        public async Task<GetUserPhonesDto> Handle(GetDefaultPhoneQuery request, CancellationToken cts)
        {
            var phone = await unitOfWork.UserPhoneRepository.GetDefaultAsync(request.UserId, cts);

            if(phone is null)
                throw new NotFoundException(nameof(UserPhone), request.UserId);

            return mapper.Map<GetUserPhonesDto>(phone);
        }
    }
}
