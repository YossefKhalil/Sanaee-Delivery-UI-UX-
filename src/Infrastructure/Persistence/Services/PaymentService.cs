

using Core.Application.Common.Contracts;

namespace Infrastructure.Persistence.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task ProcessCustomerCancellationRefundAsync(int OrderId, object CustomerId)
        {
            await Task.CompletedTask;
        }

        public async Task RefundOrderAsync(int OrderId, int UserId)
        {
            await Task.CompletedTask;
        }
    }
}
