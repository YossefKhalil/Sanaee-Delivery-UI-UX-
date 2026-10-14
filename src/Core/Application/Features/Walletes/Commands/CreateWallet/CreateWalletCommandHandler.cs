

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Walletes.Commands.CreateWallet
{
    public sealed class CreateWalletCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateWalletCommand, int>
    {
        public async Task<int> Handle(CreateWalletCommand request, CancellationToken cts)
        {
            var isDefiend = await unitOfWork.WalletRepository.IsDefinedAsync(request.UserId, cts);

            if (isDefiend)
                throw new ConflictException("There are a wallet defiend for this user");

            var wallet = new Wallet(request.UserId);

            await unitOfWork.Repository<Wallet>().AddAsync(wallet, cts);
            await unitOfWork.CompleteAsync(cts);

            return wallet.WalletId;
        }
    }
}
