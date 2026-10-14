using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Application.Features.Pymentes.Commands.RefundPayment;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.Pymentes.Commands.RefundPayment
{
    public sealed class RefundPaymentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RefundPaymentCommand>
    {
        public async Task Handle(RefundPaymentCommand request, CancellationToken cts)
        {
            var payment =
                await unitOfWork.Repository<Payment>().GetByIdAsync(request.PaymentId, cts);

            if (payment is null)
                throw new NotFoundException(nameof(Payment), request.PaymentId);

            payment.Refund();

            await unitOfWork.CompleteAsync(cts);
        }
    }
}
