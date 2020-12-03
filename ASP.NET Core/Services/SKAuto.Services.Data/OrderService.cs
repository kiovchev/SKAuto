namespace SKAuto.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Common;
    using SKAuto.Common.DtoModels.CartDtos;
    using SKAuto.Common.DtoModels.OrderDtos;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Services.Mapping.OrderServiceMappers;

    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orders;
        private readonly IRecipientService recipientService;
        private readonly IItemService itemService;
        private readonly IOrderStatusService orderStatusService;

        public OrderService(
                            IRepository<Order> orders,
                            IRecipientService recipientService,
                            IItemService itemService,
                            IOrderStatusService orderStatusService)
        {
            this.orders = orders;
            this.recipientService = recipientService;
            this.itemService = itemService;
            this.orderStatusService = orderStatusService;
        }

        public async Task<int> CreateOrderAsync(CartOrderCreateDtoModel dtoModel)
        {
            var recipient = await this.recipientService.GetRecipientByIdAsync(dtoModel.RecipientId);
            var items = await this.itemService.GetItemsByItemsIdsAsync(dtoModel.ItemsIds);
            var orderStatus = await this.orderStatusService.GetOrderStatusByName(GlobalConstants.PendingStatus);

            var currentOrder = new Order()
            {
                IssuedOn = DateTime.UtcNow.AddHours(2),
                OrderStatus = orderStatus,
                Recipient = recipient,
                Items = items,
            };

            // add orderId and set isordered to be true
            this.itemService.UpdateItems(items, currentOrder);

            await this.orders.AddAsync(currentOrder);
            await this.orders.SaveChangesAsync();

            return currentOrder.Id;
        }

        public async Task<LastOrderDto> GetLastOrderAsync(int orderId)
        {
            var lastOrder = await this.orders.All()
                                             .Include(x => x.Recipient)
                                             .Include(x => x.OrderStatus)
                                             .FirstOrDefaultAsync(x => x.Id == orderId);

            var orderItems = await this.itemService.GetItemsByOrderIdAsync(orderId);

            LastOrderDto neededOrder = null;

            if (lastOrder != null)
            {
                neededOrder = OrderServiceLastOrderMapper.Map(lastOrder, orderItems);
            }

            return neededOrder;
        }
    }
}
