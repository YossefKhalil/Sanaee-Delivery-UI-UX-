using AutoMapper;
using Core.Application.Features.Pymentes.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Features.Pymentes.Profiles
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<Payment, GetPaymentDto>()

                // User Data
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));
        }
    }
}
