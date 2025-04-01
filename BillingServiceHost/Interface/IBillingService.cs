using BillingCore.Models;

namespace BillingServiceHost.Services
{
    public interface IBillingService
    {
        Receipt ProcessOrder(Order order);
    }
}
