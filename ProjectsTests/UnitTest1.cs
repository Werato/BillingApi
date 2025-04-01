using BillingCore.Models;
using BillingServiceHost.Services;
using Moq;
using System;
using Xunit;

namespace ProjectsTests
{
    public class UnitTest1
    {
        [Fact]
        public void ProcessOrder_UsesGatewayMock_ReturnsReceipt()
        {
            // Arrange
            var mockGateway = new Mock<IPaymentGateway>();
            mockGateway.Setup(g => g.ProcessPayment(It.IsAny<Order>())).Returns(true);

            var billingService = new BillingService(mockGateway.Object);

            var order = new Order
            {
                OrderNumber = "ORD-MOCK",
                UserId = "mock-user",
                Amount = 50.0m,
                PaymentGateway = "paypal",
                Description = "mock test"
            };

            // Act
            var receipt = billingService.ProcessOrder(order);

            // Assert
            Assert.NotNull(receipt);
            Assert.Equal(order.OrderNumber, receipt.OrderNumber);
            Assert.Equal(order.Amount, receipt.AmountPaid);

            // Verify gateway was called
            mockGateway.Verify(g => g.ProcessPayment(It.IsAny<Order>()), Times.Once);
        }
    }
}
