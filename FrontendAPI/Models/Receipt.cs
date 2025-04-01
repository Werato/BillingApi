using System;

namespace FrontendAPI.Models
{
    public class Receipt
    {
        public string ReceiptId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string PaymentGateway { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
