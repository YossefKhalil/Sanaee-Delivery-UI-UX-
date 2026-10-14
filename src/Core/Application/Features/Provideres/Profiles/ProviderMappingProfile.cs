using AutoMapper;
using Core.Application.Features.Provideres.Dtos;
using Core.Domain.Entities;


namespace Application.Features.Provideres.Profiles
{
    public class ProviderMappingProfile : Profile
    {
        public ProviderMappingProfile()
        {
            CreateMap<Provider, ProviderDto>();


            CreateMap<Provider, GetProviderForAdminDto>()
            // User Data
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.UserId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.User.AverageRating))
            .ForMember(dest => dest.TotalReviewCount, opt => opt.MapFrom(src => src.User.TotalReviewCount))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.User.UpdatedAt))
            .ForMember(dest => dest.IsUserActive, opt => opt.MapFrom(src => src.User.IsActive))

            // Provider Data
            .ForMember(dest => dest.ProviderId, opt => opt.MapFrom(src => src.ProviderId))
            .ForMember(dest => dest.NationalNumber, opt => opt.MapFrom(src => src.NationalNumber))
            .ForMember(dest => dest.IsProviderAvailable, opt => opt.MapFrom(src => src.IsAvailable))

            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProviderCategories));
        }
    }
}
