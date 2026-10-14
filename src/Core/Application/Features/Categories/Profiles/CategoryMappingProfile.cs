

using AutoMapper;
using Core.Application.Features.Categories.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Features.Categories.Profiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, GetCategoryDto>()
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory!.Name));
            
        }
    }
}
