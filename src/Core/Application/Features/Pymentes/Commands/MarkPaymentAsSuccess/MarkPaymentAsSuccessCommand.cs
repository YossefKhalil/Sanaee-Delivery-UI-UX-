using MediatR;

namespace Core.Application.Features.Pymentes.Commands.MarkPaymentAsSuccess
{
    public sealed record MarkPaymentAsSuccessCommand(int PaymentId, string TransactionReference) : IRequest;
}
