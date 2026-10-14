
using AutoMapper;
using Core.Application.Features.ProviderDocuments.Dtos;
using Core.Domain.Entities;

namespace Core.Application.Features.ProviderDocuments.Profiles
{
    public class ProviderDocumentMappingProfile : Profile
    {
        public ProviderDocumentMappingProfile()
        {
            CreateMap<ProviderDocumentDto, ProviderDocument>();

            CreateMap<ProviderDocument, GetDocumentDto>();
        }
    }
}
