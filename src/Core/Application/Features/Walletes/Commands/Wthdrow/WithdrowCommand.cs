

using Core.Application.Features.Walletes.Dtos;
using MediatR;

namespace Core.Application.Features.Walletes.Commands.Wthdrow
{
    public sealed record WithdrowCommand(WalleTransactiontDto dto, int UserId) : IRequest;

}
