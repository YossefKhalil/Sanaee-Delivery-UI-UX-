using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Features.WalletTransactiones.Dtos;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.WalletTransactiones.Queries.GetAllTransactionsByWalletId
{
    public sealed class GetAllTransactionsByWalletdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllTransactionsByWalletIdQuery, IReadOnlyList<GetWalletTransactionDto>>
    {
        public async Task<IReadOnlyList<GetWalletTransactionDto>> Handle(GetAllTransactionsByWalletIdQuery request, CancellationToken cts)
        {
            var walletTransactions = await unitOfWork.Repository<WalletTransaction>()
                                                     .GetQueryable()
                                                     .AsNoTracking()
                                                     .Where(wt => wt.WalletId == request.WalletId)
                                                     .ProjectTo<GetWalletTransactionDto>(mapper.ConfigurationProvider)
                                                     .ToListAsync(cts);

            return walletTransactions;
        }
    }
}
