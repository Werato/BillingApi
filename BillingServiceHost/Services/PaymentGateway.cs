using BillingCore.Models;

namespace BillingServiceHost.Services
{
    public class PaymentGateway : IPaymentGateway
    {
        public bool ProcessPayment(Order order)
        {
            return true;
        }
    }
}
