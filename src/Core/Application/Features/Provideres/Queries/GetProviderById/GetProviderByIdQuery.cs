
using Core.Application.Features.Provideres.Dtos;
using MediatR;

namespace Core.Application.Features.Provideres.Queries.GetProviderById
{
    public sealed record GetProviderByIdQuery(int ProviderId) : IRequest<ProviderDto>;
    
}
