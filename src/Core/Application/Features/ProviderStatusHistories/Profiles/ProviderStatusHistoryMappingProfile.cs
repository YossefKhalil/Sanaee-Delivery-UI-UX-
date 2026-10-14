

using AutoMapper;
using Core.Application.Features.ProviderStatusHistories.Dto;
using Core.Domain.Entities;

namespace Core.Application.Features.ProviderStatusHistories.Profiles
{
    public class ProviderStatusHistoryMappingProfile : Profile
    {
        public ProviderStatusHistoryMappingProfile()
        {
            CreateMap<ProviderStatusHistory, GetProviderStatusHistoriesDto>();
        }
    }
}
