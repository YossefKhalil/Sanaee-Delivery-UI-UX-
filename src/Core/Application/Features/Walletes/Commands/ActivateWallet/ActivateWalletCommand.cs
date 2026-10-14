

using MediatR;

namespace Core.Application.Features.Walletes.Commands.ActivateWallet
{
    public sealed record ActivateWalletCommand(int WalletId, int UserId, string IpAddress, string UserAgent) : IRequest;
    
}
