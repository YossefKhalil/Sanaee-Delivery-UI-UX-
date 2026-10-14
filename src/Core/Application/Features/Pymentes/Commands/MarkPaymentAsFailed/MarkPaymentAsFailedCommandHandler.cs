using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Pymentes.Commands.MarkPaymentAsFailed
{
    public sealed class MarkPaymentAsFailedCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<MarkPaymentAsFailedCommand>
    {
        public async Task Handle(MarkPaymentAsFailedCommand request, CancellationToken cts)
        {
            var payment = 
                await unitOfWork.Repository<Payment>().GetByIdAsync(request.PaymentId, cts);

            if (payment is null)
                throw new NotFoundException(nameof(Payment), request.PaymentId);

            payment.MarkAsFailed();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
