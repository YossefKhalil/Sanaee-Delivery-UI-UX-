

using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.ApplicationHistories.Commands.CreateApplicationHistory
{
    public sealed class CreateApplicationHistoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateApplicationHistoryCommand, int>
    {
        public async Task<int> Handle(CreateApplicationHistoryCommand request, CancellationToken cts)
        {
            var applicationHistory = new ApplicationHistory
            {
                Status = request.dto.Status,
                Notes = request.dto.Notes,
                ProviderId = request.dto.ProviderId,
                ChangedBy = request.ChangedBy,
                ChangedAt = DateTime.UtcNow
            };

            await unitOfWork.Repository<ApplicationHistory>().AddAsync(applicationHistory, cts);
            await unitOfWork.CompleteAsync(cts);

            return applicationHistory.HistoryId;
        }
    }
}
