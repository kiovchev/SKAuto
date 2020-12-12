namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrderStatusService : IOrderStatusService
    {
        private readonly IRepository<OrderStatus> orders;

        public OrderStatusService(IRepository<OrderStatus> orders)
        {
            this.orders = orders;
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
