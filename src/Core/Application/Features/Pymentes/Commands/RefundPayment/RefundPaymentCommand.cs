using MediatR;

namespace Core.Application.Features.Pymentes.Commands.RefundPayment
{
    public sealed record RefundPaymentCommand(int PaymentId) : IRequest;
}
