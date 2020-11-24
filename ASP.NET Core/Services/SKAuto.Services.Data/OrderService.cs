namespace SKAuto.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using SKAuto.Common;
    using SKAuto.Common.DtoModels.CartDtos;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;

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
                IssuedOn = DateTime.UtcNow.AddDays(2),
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
    }
}
