using Core.Domain.Enums;
using MediatR;

namespace Core.Application.Features.Pymentes.Commands.CreatePayment
{
    public sealed record CreatePaymentCommand
        (
            decimal Amount,
            PaymentMethod Method,
            PaymentType Type,
            int UserId,
            int? OrderId

        ) : IRequest<int>;
    
}
