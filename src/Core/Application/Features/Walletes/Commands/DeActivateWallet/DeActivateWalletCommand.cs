

using MediatR;

namespace Core.Application.Features.Walletes.Commands.DeActivateWallet
{
    public sealed record DeActivateWalletCommand(int WalletId, int UserId, string IpAddress, string UserAgent) : IRequest;
    
}
