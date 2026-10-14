using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Commands.DeactivateCacelationReason
{
    public sealed class DeactivateCancelationReasonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeactivateCancelationReasonCommand>
    {
        public async Task Handle(DeactivateCancelationReasonCommand request, CancellationToken cts)
        {
            var cancelationReason =
                await unitOfWork.Repository<CancelationReason>().GetByIdAsync(request.ReasonId, cts);

            if (cancelationReason is null)
                throw new NotFoundException(nameof(CancelationReason), request.ReasonId);

            cancelationReason.Freeze();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
