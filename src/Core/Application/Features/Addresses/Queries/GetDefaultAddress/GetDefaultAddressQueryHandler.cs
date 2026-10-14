
using AutoMapper;
using Core.Application.Features.Addresses.Dtos;
using Core.Application.Common.Contracts.Persistence;
using MediatR;

namespace Core.Application.Features.Addresses.Queries.GetDefaultAddress
{
    public class GetDefaultAddressQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetDefaultAddressQuery, AddressDto>
    {
        public async Task<AddressDto> Handle(GetDefaultAddressQuery request, CancellationToken cts)
        {

            var address = await unitOfWork.AddressRepository.GetDefaultAddressAsync(request.UserId, cts);

            return mapper.Map<AddressDto>(address);

        }
    }
}