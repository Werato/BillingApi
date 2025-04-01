using BillingCore.Models;

namespace BillingServiceHost.Services
{
    public interface IPaymentGateway
    {
        bool ProcessPayment(Order order);
    }
}
