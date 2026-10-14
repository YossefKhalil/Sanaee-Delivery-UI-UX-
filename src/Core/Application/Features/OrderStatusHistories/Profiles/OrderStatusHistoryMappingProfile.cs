using AutoMapper;
using Core.Application.Features.OrderStatusHistories.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Features.OrderStatusHistories.Profiles
{
    public class OrderStatusHistoryMappingProfile : Profile
    {
        public OrderStatusHistoryMappingProfile()
        {
            CreateMap<OrderStatusHistory, GetHistoryDto>()

                // History Data
                .ForMember(dest => dest.HistoryId, opt => opt.MapFrom(src => src.HistoryId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))

                // Order Data
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))

                // Reason Data
                .ForMember(dest => dest.ReasonId, opt => opt.MapFrom(src => src.ReasonId))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Reason!.Text))

                // User Data
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));

        }
    }
}
