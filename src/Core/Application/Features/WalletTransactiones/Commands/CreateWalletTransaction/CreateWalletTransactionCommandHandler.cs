using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.WalletTransactiones.Commands.CreateWalletTransaction
{
    internal class CreateWalletTransactionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateWalletTransactionCommand, int>
    {
        public async Task<int> Handle(CreateWalletTransactionCommand request, CancellationToken cts)
        {
            var walletTransaction = new WalletTransaction
                (
                    request.Amount,
                    request.Type,
                    request.Description,
                    request.WalletId,
                    request.OrderId
                );

            await unitOfWork.Repository<WalletTransaction>().AddAsync(walletTransaction, cts);
            await unitOfWork.CompleteAsync(cts);

            return walletTransaction.TransactionId;
        }
    }
}
