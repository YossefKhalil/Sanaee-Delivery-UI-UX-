

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Walletes.Commands.Wthdrow
{
    public sealed class WithdrowCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<WithdrowCommand>
    {
        public async Task Handle(WithdrowCommand request, CancellationToken cts)
        {
            var wallet = await unitOfWork.Repository<Wallet>().GetByIdAsync(request.dto.WalletId, cts);

            if (wallet is null || request.UserId != wallet.UserId)
                throw new NotFoundException(nameof(Wallet), request.dto.WalletId);

            wallet.Withdraw(request.dto.Amount);

            var transaction = new WalletTransaction
            (
                request.dto.Amount,
                TransactionType.Withdrawal,
                $"Withdrowal operation in wallet #{request.dto.WalletId}",
                request.dto.WalletId
            );

            wallet.WalletTransactions.Add(transaction);

            try
            {
                await unitOfWork.CompleteAsync(cts);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ConflictException("The opration conflicted with another operation. Try Again");
            }
        }
    }
}
