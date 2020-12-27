namespace SKAutoNew.Services.Tests.ServicesTestHelpers
{
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;

    public static class GetOrderService
    {
        public static OrderService Return(SKAutoDbContext db) 
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
