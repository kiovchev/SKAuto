namespace SKAutoNew.Services.Tests
{
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Threading.Tasks;
    using Xunit;

    public class OrderStatusServiceTests
    {
        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task GetAllOrderStatusesNamesAsyncShouldReturnOne()
        {
            var orderStatusRepo = new Repository<OrderStatus>(db);
            var orderStatusService = new OrderStatusService(orderStatusRepo);

            await orderStatusService.CreateOrderStatusAsync("Test");
            var allOrderSatatuses = await orderStatusService.GetAllOrderStatusesNamesAsync();

            var expected = 1;
            var actual = allOrderSatatuses.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetOrderStatusByNameShouldReturnOne()
        {
            var orderStatusRepo = new Repository<OrderStatus>(db);
            var orderStatusService = new OrderStatusService(orderStatusRepo);

            await orderStatusService.CreateOrderStatusAsync("Test");
            var currentOrderSatatus = await orderStatusService.GetOrderStatusByName("Test");

            var expected = 1;
            var actual = currentOrderSatatus.Id;

            Assert.Equal(expected, actual);
        }
    }
}
