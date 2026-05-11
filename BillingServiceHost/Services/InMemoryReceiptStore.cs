using System.Collections.Concurrent;
using BillingCore.Models;
using BillingServiceHost.Interface;

namespace BillingServiceHost.Services
{
    public class InMemoryReceiptStore : IReceiptStore
    {
        private readonly ConcurrentDictionary<string, Receipt> _receipts =
            new ConcurrentDictionary<string, Receipt>();

        public bool TryGetReceipt(string orderNumber, out Receipt receipt)
        {
            return _receipts.TryGetValue(orderNumber, out receipt);
        }

        public void SaveReceipt(string orderNumber, Receipt receipt)
        {
            _receipts.TryAdd(orderNumber, receipt);
        }
    }
}