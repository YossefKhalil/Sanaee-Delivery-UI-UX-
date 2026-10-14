
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Walletes.Commands.Deposit
{
    public class DepositCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DepositCommand>
    {
        public async Task Handle(DepositCommand request, CancellationToken cts)
        {
            var wallet = await unitOfWork.Repository<Wallet>().GetByIdAsync(request.dto.WalletId, cts);

            if (wallet is null || request.UserID != wallet.UserId)
                throw new NotFoundException(nameof(Wallet), request.dto.WalletId);

            wallet.Deposit(request.dto.Amount);

            var transaction = new WalletTransaction
            (
                 request.dto.Amount,
                 TransactionType.Deposit,
                $"Deposit operation in wallet #{request.dto.WalletId}",
                request.dto.WalletId
            );

            wallet.WalletTransactions.Add(transaction);

            try
            {
                await unitOfWork.CompleteAsync(cts);
            }
            catch(DbUpdateConcurrencyException)
            {
                throw new ConflictException("The opration conflicted with another operation. Try Again");
            }
        }
    }
}
