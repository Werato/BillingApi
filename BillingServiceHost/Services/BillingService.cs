using BillingCore.Models;
using BillingServiceHost.Interface;
using System;

namespace BillingServiceHost.Services
{
    public class BillingService : IBillingService
    {
        private static readonly object _lock = new object();

        private readonly IPaymentGateway _paymentGateway;
        private readonly IReceiptStore _receiptStore;

        public BillingService(
            IPaymentGateway paymentGateway,
            IReceiptStore receiptStore)
        {
            _paymentGateway = paymentGateway;
            _receiptStore = receiptStore;
        }

        public Receipt ProcessOrder(Order order)
        {
            // TODO: transaction-level locking.
            lock (_lock)
            {
                if (_receiptStore.TryGetReceipt(order.OrderNumber, out var existingReceipt))
                {
                    return existingReceipt;
                }

                var paymentSuccessful = _paymentGateway.ProcessPayment(order);

                if (!paymentSuccessful)
                {
                    throw new Exception("Payment failed");
                }

                var receipt = new Receipt
                {
                    ReceiptId = Guid.NewGuid().ToString(),
                    OrderNumber = order.OrderNumber,
                    PaymentGateway = order.PaymentGateway,
                    Amount = order.Amount,
                    CreatedAt = DateTime.UtcNow
                };

                _receiptStore.SaveReceipt(order.OrderNumber, receipt);

                return receipt;
            }
        }
    }
}
