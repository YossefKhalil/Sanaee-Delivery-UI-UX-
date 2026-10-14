

using AutoMapper;
using Core.Application.Features.Offeres.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Features.Offeres.Profiles
{
    public class OfferMappingProfile : Profile
    {
        public OfferMappingProfile()
        {
            CreateMap<Offer, GetOfferDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Provider.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Provider.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Provider.User.Email))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Provider.User.AverageRating))
                .ForMember(dest => dest.TotalReviewCount, opt => opt.MapFrom(src => src.Provider.User.TotalReviewCount));


            CreateMap<Offer, GetAllOffersForOrderDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Provider.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Provider.User.LastName));
        }
    }
}
