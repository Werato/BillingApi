namespace BillingCore.Models
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentGateway { get; set; }
        public string Description { get; set; }
    }
}
