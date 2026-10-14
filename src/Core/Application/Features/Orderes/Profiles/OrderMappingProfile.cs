

using AutoMapper;
using Core.Application.Features.Orderes.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Features.Orderes.Profiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, GetOrderDto>()

                // Order Data
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProblemDescription, opt => opt.MapFrom(src => src.ProblemDescription))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.IsClientReviewed, opt => opt.MapFrom(src => src.IsClientReviewed))
                .ForMember(dest => dest.IsProviderReviewed, opt => opt.MapFrom(src => src.IsProviderReviewed))

                // Category Data
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))

                // 2. Provider Data
                .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Address.Title))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Address.Region))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))

                // 3. User Data
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))

                // Offer Data
                .ForMember(dest => dest.AcceptedOfferId, opt => opt.MapFrom(src => src.AcceptedOfferId))
                .ForMember(dest => dest.ProviderId, opt => opt.MapFrom(src => src.Offer!.ProviderId));



            CreateMap<Order, GetAllOrdersDto>()

                // Order Data
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.IsClientReviewed, opt => opt.MapFrom(src => src.IsClientReviewed))
                .ForMember(dest => dest.IsProviderReviewed, opt => opt.MapFrom(src => src.IsProviderReviewed))

                // Category Data
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))

                // 2. Provider Data
                .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Address.Title))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Address.Region))

                // 3. User Data
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))

                // Offer Data
                .ForMember(dest => dest.AcceptedOfferId, opt => opt.MapFrom(src => src.AcceptedOfferId))
                .ForMember(dest => dest.ProviderId, opt => opt.MapFrom(src => src.Offer!.ProviderId));



            CreateMap<Order, GetAllOrdersForCustomerDto>()

                // Order Data
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))

                // Category Data
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))

                // 3. User Data
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))

                // Offer Data
                .ForMember(dest => dest.AcceptedOfferId, opt => opt.MapFrom(src => src.AcceptedOfferId))
                .ForMember(dest => dest.ProviderId, opt => opt.MapFrom(src => src.Offer!.ProviderId));
        }
    }
}
