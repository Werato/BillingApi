using BillingCore.Models;
using BillingServiceHost.Interface;
using BillingServiceHost.Services;
using Moq;
using Xunit;

namespace ProjectsTests
{
    public class BillingServiceTests
    {
        [Fact]
        public void ProcessOrder_WhenPaymentSuccessful_ReturnsReceipt()
        {
            var paymentGatewayMock = new Mock<IPaymentGateway>();
            paymentGatewayMock
                .Setup(x => x.ProcessPayment(It.IsAny<Order>()))
                .Returns(true);

            var receiptStore = new InMemoryReceiptStore();
            var service = new BillingService(paymentGatewayMock.Object, receiptStore);

            var order = new Order
            {
                OrderNumber = "ORD-001",
                UserId = "user-1",
                Amount = 50m,
                PaymentGateway = "paypal",
                Description = "test order"
            };

            var receipt = service.ProcessOrder(order);

            Assert.NotNull(receipt);
            Assert.Equal(order.OrderNumber, receipt.OrderNumber);
            Assert.Equal(order.Amount, receipt.Amount);
            paymentGatewayMock.Verify(x => x.ProcessPayment(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void ProcessOrder_WhenSameOrderProcessedTwice_ReturnsSameReceiptAndDoesNotChargeTwice()
        {
            var paymentGatewayMock = new Mock<IPaymentGateway>();
            paymentGatewayMock
                .Setup(x => x.ProcessPayment(It.IsAny<Order>()))
                .Returns(true);

            var receiptStore = new InMemoryReceiptStore();
            var service = new BillingService(paymentGatewayMock.Object, receiptStore);

            var order = new Order
            {
                OrderNumber = "ORD-IDEMPOTENT",
                UserId = "user-1",
                Amount = 100m,
                PaymentGateway = "paypal",
                Description = "idempotency test"
            };

            var firstReceipt = service.ProcessOrder(order);
            var secondReceipt = service.ProcessOrder(order);

            Assert.Equal(firstReceipt.ReceiptId, secondReceipt.ReceiptId);
            paymentGatewayMock.Verify(x => x.ProcessPayment(It.IsAny<Order>()), Times.Once);
        }
    }
}