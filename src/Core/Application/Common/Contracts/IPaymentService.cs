

namespace Core.Application.Common.Contracts
{
    public interface IPaymentService
   {
        Task ProcessCustomerCancellationRefundAsync(int OrderId, object CustomerId);
        Task RefundOrderAsync(int OrderId, int UserId);
    }
}
