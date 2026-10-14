using Core.Application.Common.Contracts.Persistence;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.CancelatoinReasones.Commands.CreateCancelationReason
{
    public sealed class CreateCancelationReasonCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCancelationReasonCommand, int>
    {
        public async Task<int> Handle(CreateCancelationReasonCommand request, CancellationToken cts)
        {
            var cancelationReason = new CancelationReason
            {
                Text = request.Text,
                ApplicableTo = request.ApplicableTo,
            };

            await unitOfWork.Repository<CancelationReason>().AddAsync(cancelationReason, cts);
            await unitOfWork.CompleteAsync(cts);

            return cancelationReason.ReasonId;
        }
    }
}
