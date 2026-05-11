using BillingCore.Models;

namespace BillingServiceHost.Interface
{
    public interface IReceiptStore
    {
        bool TryGetReceipt(string orderNumber, out Receipt receipt);
        void SaveReceipt(string orderNumber, Receipt receipt);
    }
}
