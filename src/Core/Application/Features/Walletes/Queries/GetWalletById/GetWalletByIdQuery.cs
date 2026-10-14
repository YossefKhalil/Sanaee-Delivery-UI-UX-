

using MediatR;

namespace Core.Application.Features.Walletes.Queries.GetWalletById
{
    public sealed record GetWalletByIdQuery(int WalletId, int UserId) : IRequest<decimal>;
    
}
