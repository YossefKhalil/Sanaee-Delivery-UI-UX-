
using AutoMapper;
using Core.Application.Features.Addresses.Dtos;
using Core.Application.Common.Contracts.Persistence;
using MediatR;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;

namespace Core.Application.Features.Addresses.Queries.GetUserAddresses
{
    public class GetUserAddressesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetUserAddressesQuery, IReadOnlyList<AddressDto>>
    {
        public async Task<IReadOnlyList<AddressDto>> Handle(GetUserAddressesQuery request, CancellationToken cts)
        {

            var addresses = await unitOfWork.AddressRepository.GetUserAddressesAsync(request.UserId, cts);

            if (addresses.Count == 0)
                throw new NotFoundException(nameof(Address), request.UserId);

            return mapper.Map<IReadOnlyList<AddressDto>>(addresses);

        }
    }
}