
using Core.Application.Features.Walletes.Dtos;
using MediatR;

namespace Core.Application.Features.Walletes.Commands.Deposit
{
    public sealed record DepositCommand(WalleTransactiontDto dto, int UserID) : IRequest;
    
}
