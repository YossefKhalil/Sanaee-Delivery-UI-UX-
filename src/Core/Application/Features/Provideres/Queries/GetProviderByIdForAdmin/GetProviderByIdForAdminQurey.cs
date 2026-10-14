using Core.Application.Features.Provideres.Dtos;
using MediatR;

namespace Core.Application.Features.Provideres.Queries.GetProviderByIdForAdmin
{
    public sealed record GetProviderByIdForAdminQurey(int ProviderId) : 
        IRequest<GetProviderForAdminDto>;
}
