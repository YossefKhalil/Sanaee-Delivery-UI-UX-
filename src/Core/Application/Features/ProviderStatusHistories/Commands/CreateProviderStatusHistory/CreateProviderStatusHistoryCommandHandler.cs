

using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.ProviderStatusHistories.Commands.CreateProviderStatusHistory
{
    public sealed class CreateProviderStatusHistoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProviderStatusHistoryCommand, int>
    {
        public async Task<int> Handle(CreateProviderStatusHistoryCommand request, CancellationToken cts)
        {
            var statusHistory = new ProviderStatusHistory
            {
                Status = request.dto.Status,
                Notes = request.dto.Notes,
                ProviderId = request.dto.ProviderId,
                ChangedAt = DateTime.UtcNow,
                ChangedBy = request.AdminId
            };

            await unitOfWork.Repository<ProviderStatusHistory>().AddAsync(statusHistory, cts);
            await unitOfWork.CompleteAsync(cts);

            return statusHistory.HistoryId;
        }
    }
}
