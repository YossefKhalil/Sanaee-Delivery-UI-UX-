using Core.Application.Features.Walletes.Queries.GetWalletById;
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;


namespace Application.Features.Walletes.Queries.GetWalletById
{
    public sealed class GetWalletByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetWalletByIdQuery, decimal>
    {
        public async Task<decimal> Handle(GetWalletByIdQuery request, CancellationToken cts)
        {
            var wallet = await unitOfWork.Repository<Wallet>().GetByIdAsync(request.WalletId, cts);

            if (wallet is null || request.UserId != wallet.UserId || !wallet.IsActive)
                throw new NotFoundException(nameof(Wallet), request.WalletId);

            return wallet.Balance;
        }
    }
}
