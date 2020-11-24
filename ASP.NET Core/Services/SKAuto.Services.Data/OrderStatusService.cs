namespace SKAuto.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;

    public class OrderStatusService : IOrderStatusService
    {
        private readonly IRepository<OrderStatus> orders;

        public OrderStatusService(IRepository<OrderStatus> orders)
        {
            this.orders = orders;
        }

        public async Task<OrderStatus> GetOrderStatusByName(string name)
        {
            var orderStatus = await this.orders.AllAsNoTracking().FirstOrDefaultAsync(x => x.Name == name);

            return orderStatus;
        }
    }
}
