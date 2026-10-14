

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Core.Application.Features.CancelatoinReasones.Commands.ActivateCancelationReason
{
    public sealed class ActivateCancelationReasonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ActivateCancelationReasonCommand>
    {
        public async Task Handle(ActivateCancelationReasonCommand request, CancellationToken cts)
        {
            var cancelationReason =
                await unitOfWork.Repository<CancelationReason>().GetByIdAsync(request.ReasonId, cts);

            if (cancelationReason is null)
                throw new NotFoundException(nameof(CancelationReason), request.ReasonId);

            cancelationReason.Unfreeze();
            
            await unitOfWork.CompleteAsync(cts);
        }
    }
}
