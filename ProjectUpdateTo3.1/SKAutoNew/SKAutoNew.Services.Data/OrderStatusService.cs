namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// business logic for OrderStatus
    /// </summary>
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IRepository<OrderStatus> orders;

        public OrderStatusService(IRepository<OrderStatus> orders)
        {
            this.orders = orders;
        }

        // this method is created for tests
        public async Task CreateOrderStatusAsync(string orderStatusName)
        {
            var orderStatus = new OrderStatus 
            {
                Name = orderStatusName
            };
            await orders.InsertAsync(orderStatus);
            await orders.SaveAsync();
        }

        public async Task<IList<string>> GetAllOrderStatusesNamesAsync()
        {
            var allOrderStatusesNames = await this.orders.All().Select(x => x.Name).ToListAsync();

            return allOrderStatusesNames;
        }

        public async Task<OrderStatus> GetOrderStatusByName(string name)
        {
            var orderStatus = await this.orders.AllAsNoTracking().FirstOrDefaultAsync(x => x.Name == name);

            return orderStatus;
        }
    }
}
