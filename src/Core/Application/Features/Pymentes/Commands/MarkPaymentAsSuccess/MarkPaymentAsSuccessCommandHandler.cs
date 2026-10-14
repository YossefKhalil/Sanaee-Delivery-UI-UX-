using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using MediatR;

namespace Core.Application.Features.Pymentes.Commands.MarkPaymentAsSuccess
{
    public sealed class MarkPaymentAsSuccessCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<MarkPaymentAsSuccessCommand>
    {
        public async Task Handle(MarkPaymentAsSuccessCommand request, CancellationToken cts)
        {
            var payment =
                await unitOfWork.Repository<Payment>().GetByIdAsync(request.PaymentId, cts);

            if (payment is null)
                throw new NotFoundException(nameof(Payment), request.PaymentId);

            payment.MarkAsSuccess(request.TransactionReference);

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
