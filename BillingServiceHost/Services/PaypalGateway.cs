using BillingCore.Models;

namespace BillingServiceHost.Services
{
    public class PaypalGateway : IPaymentGateway
    {
        public bool ProcessPayment(Order order)
        {
            return true;
        }
    }
}
