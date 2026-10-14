

using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.CancelatoinReasones.Commands.UpdateCancelationReason;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.CancelatoinReasones.Commands.UpdateCancelationReason
{
    public sealed class UpdateCancelationReasonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCancelationReasonCommand>
    {
        public async Task Handle(UpdateCancelationReasonCommand request, CancellationToken cts)
        {
            var cancelationReason =
                await unitOfWork.Repository<CancelationReason>().GetByIdAsync(request.ReasonId, cts);

            if (cancelationReason is null)
                throw new NotFoundException(nameof(CancelationReason), request.ReasonId);


            if (!string.IsNullOrWhiteSpace(request.Text))
                cancelationReason.Text = request.Text;

            if (request.ApplicableTo is not null)
                cancelationReason.ApplicableTo = request.ApplicableTo.Value;


            unitOfWork.Repository<CancelationReason>().Update(cancelationReason);
            await unitOfWork.CompleteAsync(cts);
        }
    }
}
