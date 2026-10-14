
using AutoMapper;
using Core.Application.Features.ProviderCategories.Dtos;
using Core.Application.Features.Provideres.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Features.ProviderCategories.Profiles
{
    public class ProviderCategoryMappingProfile : Profile
    {
        public ProviderCategoryMappingProfile()
        {
            CreateMap<ProviderCategoryDto, ProviderCategory>();



            CreateMap<ProviderCategory, GetProviderCategoryDto>()
            // 1. Category Data
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Category.Description))
            .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.Category.IconUrl))

            // 2. Provider Data
            .ForMember(dest => dest.ProviderId, opt => opt.MapFrom(src => src.Provider.ProviderId))
            .ForMember(dest => dest.NationalNumber, opt => opt.MapFrom(src => src.Provider.NationalNumber))
            .ForMember(dest => dest.IsProviderAvailable, opt => opt.MapFrom(src => src.Provider.IsAvailable))

            // 3. User Data
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Provider.UserId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Provider.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Provider.User.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Provider.User.Email))
            .ForMember(dest => dest.IsUserActive, opt => opt.MapFrom(src => src.Provider.User.IsActive))
            .ForMember(dest => dest.TotalReviewCount, opt => opt.MapFrom(src => src.Provider.User.TotalReviewCount))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Provider.User.AverageRating))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Provider.User.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Provider.User.UpdatedAt));


            CreateMap<ProviderCategory, CategoryDtoHelper>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Category.Description))
                .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.Category.IconUrl));
        }

    }
}
