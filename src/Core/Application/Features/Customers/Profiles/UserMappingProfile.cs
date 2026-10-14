using AutoMapper;
using Core.Application.Features.Customers.Dtos;
using Core.Domain.Entities;


namespace Application.Features.Customers.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {

            CreateMap<User, UserDto>();
        }
    }
}
