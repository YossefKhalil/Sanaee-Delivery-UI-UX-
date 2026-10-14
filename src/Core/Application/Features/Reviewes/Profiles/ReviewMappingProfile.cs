using Application.Features.Reviewes.Dto;
using AutoMapper;
using Core.Application.Features.Reviewes.Dto;
using Core.Domain.Entities;

namespace Core.Application.Features.Reviewes.Profiles
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<Review, GetReviewByIdDto>();
            CreateMap<Review, GetReviewsByRevieweeIdDto>();
        }
    }
}
