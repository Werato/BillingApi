using BillingCore.Models;
using System;

namespace BillingServiceHost.Services
{
    public class BillingService : IBillingService
    {
        private readonly IPaymentGateway _gateway;

        public BillingService(IPaymentGateway gateway)
        {
            _gateway = gateway;
        }

        public Receipt ProcessOrder(Order order)
        {
            if (!_gateway.ProcessPayment(order))
                throw new Exception("Payment failed");

            return new Receipt
            {
                ReceiptId = Guid.NewGuid().ToString(),
                OrderNumber = order.OrderNumber,
                Timestamp = DateTime.UtcNow,
                PaymentGateway = order.PaymentGateway,
                AmountPaid = order.Amount
            };
        }
    }
}
