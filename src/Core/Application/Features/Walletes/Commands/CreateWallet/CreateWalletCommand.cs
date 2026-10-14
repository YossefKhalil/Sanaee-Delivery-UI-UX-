

using MediatR;

namespace Core.Application.Features.Walletes.Commands.CreateWallet
{
    public sealed record CreateWalletCommand(int UserId) : IRequest<int>;
    
}
