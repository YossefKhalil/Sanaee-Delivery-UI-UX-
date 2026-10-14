using MediatR;

namespace Core.Application.Features.Pymentes.Commands.MarkPaymentAsFailed
{
    public sealed record MarkPaymentAsFailedCommand(int PaymentId) : IRequest;
}
