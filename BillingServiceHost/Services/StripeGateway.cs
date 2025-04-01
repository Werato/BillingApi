using BillingCore.Models;

namespace BillingServiceHost.Services
{
    public class StripeGateway : IPaymentGateway
    {
        public bool ProcessPayment(Order order)
        {
            return true;
        }
    }
}
