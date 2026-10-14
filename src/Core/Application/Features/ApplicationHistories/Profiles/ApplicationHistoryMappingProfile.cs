
using Core.Application.Features.ApplicationHistories.Dtos;
using Core.Domain.Entities;
using AutoMapper;

namespace Core.Application.Features.ApplicationHistories.Profiles
{
    public class ApplicationHistoryMappingProfile : Profile
    {
        public ApplicationHistoryMappingProfile()
        {
            // CreateMap<ApplicationHistoryDto, ApplicationHistory>();

            CreateMap<ApplicationHistory, GetApplicationHistoriesDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));

        }
    }
}
