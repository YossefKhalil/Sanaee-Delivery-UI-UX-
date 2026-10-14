using AutoMapper;
using Core.Application.Features.Addresses.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Features.Addresses.Profiles
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            // التحويل المباشر إذا كانت أسماء الخصائص متطابقة في الكلاسين
            CreateMap<Address, AddressDto>();

             //💡   تحويل من DTO إلى Entity :
             CreateMap<CreateUadateAddressDto, Address>();

             CreateMap<CreateAddressByUserEntity, Address>();
        }
    }
}