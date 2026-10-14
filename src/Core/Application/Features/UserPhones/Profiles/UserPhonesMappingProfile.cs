using AutoMapper;
using Core.Application.Features.UserPhones.Dto;
using Core.Domain.Entities;


namespace Core.Application.Features.UserPhones.Profiles
{
    public class UserPhonesMappingProfile : Profile
    {
        public UserPhonesMappingProfile()
        {

            CreateMap<UserPhone, GetUserPhonesDto>();

        }
    }
}
