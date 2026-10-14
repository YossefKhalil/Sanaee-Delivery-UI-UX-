
using Core.Application.Features.ProviderStatusHistories.Dto;
using MediatR;

namespace Core.Application.Features.ProviderStatusHistories.Commands.CreateProviderStatusHistory
{
    /// <summary>
    ///  Auditing all changes in Provider Status by Admin
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="AdminId"></param>
    public sealed record CreateProviderStatusHistoryCommand(ProviderStatusHistoryDto dto, int AdminId) : IRequest<int>;
    
}
