namespace SKAutoNew.Services.Tests
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class OrderServiceTests
    {
        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task CreateOrderAsyncShouldReturnOne()
        {
            OrderService orderService = Return(db);

            var orderId = await orderService.CreateOrderAsync(new CartOrderCreateDtoModel
            {
                ItemsIds = new List<int> { 1, 2, 3 },
                RecipientId = 1
            });

            var expected = 1;
            var actual = orderId;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteAllOrdersByRecipientIdAsyncReturnZero()
        {
            OrderService orderService = Return(db);

            var orderId = await orderService.CreateOrderAsync(new CartOrderCreateDtoModel
            {
                ItemsIds = new List<int> { 1, 2, 3 },
                RecipientId = 1
            });

            await orderService.DeleteAllOrdersByRecipientIdAsync(1);
            var allOrders = await orderService.GetAllOrdersAsync();

            var expected = 0;
            var actual = allOrders.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteOrderAsyncShouldReturnZero()
        {
            OrderService orderService = Return(db);

            var orderId = await orderService.CreateOrderAsync(new CartOrderCreateDtoModel
            {
                ItemsIds = new List<int> { 1, 2, 3 },
                RecipientId = 1
            });

            await orderService.DeleteOrderAsync(1);
            var allOrders = await orderService.GetAllOrdersAsync();

            var expected = 0;
            var actual = allOrders.Count;

            Assert.Equal(expected, actual);
        }
        
        private OrderService Return(SKAutoDbContext db)
        {
            var ordersRepository = new Repository<Order>(db);
            var recipientRepository = new Repository<Recipient>(db);
            var orderStatusRepository = new Repository<OrderStatus>(db);
            var itemService = GetItemService.Return(db);
            var orderStatusService = new OrderStatusService(orderStatusRepository);
            var orderService = new OrderService(ordersRepository, recipientRepository, itemService, orderStatusService);
            
            return orderService;
        }
    }
}
