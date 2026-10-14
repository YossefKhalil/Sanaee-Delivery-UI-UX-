using AutoMapper;
using Core.Application.Features.Messages.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Features.Messages.Profiles
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<Message, GetMessageDto>();
        }
    }
}
