

using AutoMapper;
using Core.Application.Features.CancelatoinReasones.Dto;
using Core.Domain.Entities;

namespace Core.Application.Features.CancelatoinReasones.Profiles
{
    public class CancelationReasonMappingProfile : Profile
    {
        public CancelationReasonMappingProfile()
        {
            CreateMap<CancelationReason, GetUserCancelationReasonDto>();

            CreateMap<CancelationReason, GetUserCancelationReasonDto>();
        }
    }
}
