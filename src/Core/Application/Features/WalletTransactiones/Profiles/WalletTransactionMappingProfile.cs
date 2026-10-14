using AutoMapper;
using Core.Application.Features.WalletTransactiones.Dtos;
using Core.Domain.Entities;


namespace Core.Application.Features.WalletTransactiones.Profiles
{
    public class WalletTransactionMappingProfile : Profile
    {
        public WalletTransactionMappingProfile()
        {
            CreateMap<WalletTransaction, GetWalletTransactionDto>();
        }
    }
}
