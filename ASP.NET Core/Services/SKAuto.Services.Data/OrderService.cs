namespace SKAuto.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orders;

        public OrderService(IRepository<Order> orders)
        {
            this.orders = orders;
        }

        public async Task<Order> CreateOrderAsync(Recipient recipient, PartByCategoryAndModelViewModel partModel)
        {
            var currentOrder = new Order()
            {
                IssuedOn = DateTime.UtcNow,
                OrderStatus = new OrderStatus()
                {
                    Name = "Pending",
                },
                Recipient = recipient,
            };

            await this.orders.AddAsync(currentOrder);
            await this.orders.SaveChangesAsync();

            // must add a part - first check does part exists and is there enought quantity in database, after that do it.
            return currentOrder;
        }
    }
}
