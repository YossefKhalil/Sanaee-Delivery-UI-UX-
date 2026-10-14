
using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;

namespace Core.Application.Features.Walletes.Commands.DeActivateWallet
{
    public sealed class DeActivateWalletCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeActivateWalletCommand>
    {
        public async Task Handle(DeActivateWalletCommand request, CancellationToken cts)
        {
            var wallet = await unitOfWork.Repository<Wallet>().GetByIdAsync(request.WalletId, cts);

            if (wallet is null || request.UserId != wallet.UserId)
                throw new NotFoundException(nameof(Wallet), request.WalletId);

            wallet.Freeze();

            var log = new AuditLog
            {
                EntityName = EntityName.Wallet,
                EntityId = wallet.WalletId,
                Action = ActionType.DeActivate,
                Reason = "Deativate wallet by the user",
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
