using Core.Application.Features.Walletes.Dtos;
using Core.Domain.Entities;
using AutoMapper;


namespace Core.Application.Features.Walletes.Profiles
{
    public class WalletMappingProfile : Profile
    {
        public WalletMappingProfile()
        {
            CreateMap<Wallet, WalletDto>();
        }
    }
}
