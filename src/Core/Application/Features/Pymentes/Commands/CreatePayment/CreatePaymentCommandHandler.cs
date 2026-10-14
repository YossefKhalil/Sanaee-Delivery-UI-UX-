using Core.Application.Common.Contracts.Persistence;
using Core.Application.Common.Exceptions;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Features.Pymentes.Commands.CreatePayment
{
    public sealed class CreatePaymentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePaymentCommand, int>
    {
        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cts)
        {
            var payment = new Payment
                (
                    request.Amount,
                    request.Method,
                    request.Type,
                    request.UserId,
                    request.OrderId
                );

            await unitOfWork.Repository<Payment>().AddAsync(payment, cts);

            if (request.Method == PaymentMethod.Wallet || request.Type == PaymentType.WalletTopUp)
            {
                // جلب محفظة المستخدم
                var wallet = await unitOfWork.Repository<Wallet>().GetQueryable()
                    .FirstOrDefaultAsync(w => w.UserId == request.UserId, cts);

                if (wallet == null)
                    throw new NotFoundException("المحفظة غير موجودة لهذا المستخدم.");

                // تحديد نوع المعاملة المالية في الدفتر (Deposit لو شحن، أو OrderPayment لو خصم مقابل طلب)
                var transactionType = request.Type == PaymentType.WalletTopUp
                    ? TransactionType.Deposit
                    : TransactionType.OrderPayment;

                var description = request.Type == PaymentType.WalletTopUp
                    ? "شحن رصيد المحفظة"
                    : $"خصم قيمة الطلب رقم #{request.OrderId}";

                // إنشاء سجل معاملة المحفظة (WalletTransaction)
                var walletTransaction = new WalletTransaction
                (
                    amount: request.Amount,
                    type: transactionType,
                    description: description,
                    walletId: wallet.WalletId,
                    orderId: request.OrderId
                );

                await unitOfWork.Repository<WalletTransaction>().AddAsync(walletTransaction, cts);

                // تحديث رصيد المحفظة الفعلي
                if (transactionType == TransactionType.Deposit)
                {
                    wallet.Deposit(request.Amount); // دالة داخل كيان المحفظة لتحديث الرصيد
                }
                else
                {
                    wallet.Withdraw(request.Amount); // دالة داخل كيان المحفظة لخصم الرصيد
                }
            }

            payment.MarkAsSuccess("Internal Wallet Payment");

            await unitOfWork.CompleteAsync(cts);

            return payment.PaymentId;
        }
    }
}
