using Xunit;
using Moq;
using Store.Services;
using Store.Repositories;
using Store.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Test
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsAllOrders()
        {
            // Arrange
            var mockRepo = new Mock<IOrderRepository>();

            // Setup the repository to return some dummy orders
            mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Order>
                {
                    new Order { OrderId = 1 },
                    new Order { OrderId = 2 }
                });

            var service = new OrderService(mockRepo.Object);

            // Act
            var orders = await service.GetAllAsync();

            // Assert
            Assert.NotNull(orders);
            Assert.Equal(2, orders.Count);
            Assert.Contains(orders, o => o.OrderId == 1);
            Assert.Contains(orders, o => o.OrderId == 2);
        }
    }
}
