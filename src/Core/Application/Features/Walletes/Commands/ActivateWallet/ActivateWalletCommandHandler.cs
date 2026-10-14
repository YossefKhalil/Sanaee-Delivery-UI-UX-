
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;

namespace Core.Application.Features.Walletes.Commands.ActivateWallet
{
    public sealed class ActivateWalletCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ActivateWalletCommand>
    {
        public async Task Handle(ActivateWalletCommand request, CancellationToken cts)
        {
            var wallet = await unitOfWork.Repository<Wallet>().GetByIdAsync(request.WalletId, cts);

            if (wallet is null || request.UserId != wallet.UserId)
                throw new NotFoundException(nameof(Wallet), request.WalletId);

            wallet.Unfreeze();

            var log = new AuditLog
            {
                EntityName = EntityName.Wallet,
                EntityId = wallet.WalletId,
                Action = ActionType.Activate,
                Reason = "Restore wallet by the user",
                CreatedDate = DateTime.UtcNow,
                PerformedById = request.UserId,
                IsSelfAction = true,
                IpAddress = request.IpAddress,
                UserAgent = request.UserAgent
            };

            await unitOfWork.Repository<AuditLog>().AddAsync(log, cts);

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
