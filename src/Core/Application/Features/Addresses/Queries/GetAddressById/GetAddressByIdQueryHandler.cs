using AutoMapper;
using Core.Application.Features.Addresses.Dtos;
using Core.Application.Common.Contracts.Persistence;
using MediatR;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;

namespace Core.Application.Features.Addresses.Queries.GetAddressById
{
    public class GetAddressByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) :
        IRequestHandler<GetAddressByIdQuery, AddressDto>
    {
        public async Task<AddressDto> Handle(GetAddressByIdQuery request, CancellationToken cts)
        {
            var address = await unitOfWork.AddressRepository.GetByIdAsync(request.AddressId, cts);

            if (address == null) throw new NotFoundException(nameof(Address), request.AddressId);

            return mapper.Map<AddressDto>(address);
        }
    }
}
