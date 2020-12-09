namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using System.Threading.Tasks;

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
