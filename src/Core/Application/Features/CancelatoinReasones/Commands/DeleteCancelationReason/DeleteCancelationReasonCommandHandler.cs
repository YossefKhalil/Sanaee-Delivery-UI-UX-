using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.CancelatoinReasones.Commands.DeleteCancelationReason;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.CancelatoinReasones.Commands.DeleteCancelationReason
{
    public sealed class DeleteCancelationReasonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCancelationReasonCommand>
    {
        public async Task Handle(DeleteCancelationReasonCommand request, CancellationToken cts)
        {
            var cancelationReason =
                await unitOfWork.Repository<CancelationReason>().GetByIdAsync(request.ReasonId, cts);

            if (cancelationReason is null)
                throw new NotFoundException(nameof(CancelationReason), request.ReasonId);

            unitOfWork.Repository<CancelationReason>().Delete(cancelationReason);
            await unitOfWork.CompleteAsync(cts);
        }
    }
}
