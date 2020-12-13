namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Common.DtoModels.OrderDtos;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Mappers.OrderServiceMappers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orders;
        private readonly IRepository<Recipient> recipientRepo;
        private readonly IItemService itemService;
        private readonly IOrderStatusService orderStatusService;

        public OrderService(
                            IRepository<Order> orders,
                            IRepository<Recipient> recipientRepo,
                            IItemService itemService,
                            IOrderStatusService orderStatusService)
        {
            this.orders = orders;
            this.recipientRepo = recipientRepo;
            this.itemService = itemService;
            this.orderStatusService = orderStatusService;
        }

        // create order
        public async Task<int> CreateOrderAsync(CartOrderCreateDtoModel dtoModel)
        {
            var recipient = await this.recipientRepo.All().FirstOrDefaultAsync(x => x.Id == dtoModel.RecipientId);
            var items = await this.itemService.GetItemsByItemsIdsAsync(dtoModel.ItemsIds);
            var orderStatus = await this.orderStatusService.GetOrderStatusByName(GlobalConstants.PendingBGStatus);

            var currentOrder = new Order()
            {
                IssuedOn = DateTime.UtcNow.AddHours(2),
                OrderStatus = orderStatus,
                Recipient = recipient,
                Items = items,
            };

            // add orderId and set isordered to be true
            this.itemService.UpdateItems(items, currentOrder);

            await this.orders.InsertAsync(currentOrder);
            await this.orders.SaveAsync();

            return currentOrder.Id;
        }

        public async Task DeleteAllOrdersByRecipientIdAsync(int recipientId)
        {
            var ordersToDelete = await this.orders.All()
                                            .Include(x => x.Recipient)
                                            .Where(x => x.Recipient.Id == recipientId)
                                            .ToListAsync();

            foreach (var orderToDelete in ordersToDelete)
            {
                await this.itemService.DeleteItemsForOrderAsync(orderToDelete.Id);
                this.orders.Delete(orderToDelete);
            }
        }

        // delete order
        public async Task DeleteOrderAsync(int orderId)
        {
            var orderToDelete = await this.orders.All().FirstOrDefaultAsync(x => x.Id == orderId);

            if (orderToDelete != null)
            {
                await this.itemService.DeleteItemsForOrderAsync(orderToDelete.Id);

                this.orders.Delete(orderToDelete);
                await this.orders.SaveAsync();
            }
        }

        // get all orders 
        public async Task<IList<IndexOrderDto>> GetAllOrdersAsync()
        {
            var ordersAll = await this.orders.All()
                                       .Include(x => x.Recipient)
                                       .Include(x => x.OrderStatus)
                                       .Include(x => x.Items)
                                       .ThenInclude(x => x.Part)
                                       .ToListAsync();

            var ordersAllDtos = OrderIndexServiceMapper.Map(ordersAll);

            return ordersAllDtos;
        }

        // gets last order for current recipient
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

        // get params for order update
        public async Task<UpdateOutPutOrderDtoModel> GetUpdateOrderParamsAsync(int orderId)
        {
            var orderParamsModel = await this.orders.All()
                                       .Include(x => x.Recipient)
                                       .Include(x => x.OrderStatus)
                                       .Include(x => x.Items)
                                       .ThenInclude(x => x.Part)
                                       .FirstOrDefaultAsync(x => x.Id == orderId);

            var allOrderStatusesNames = await this.orderStatusService.GetAllOrderStatusesNamesAsync();

            var orderParamsDto = OrderUpdateParamsServiceMapper.Map(orderParamsModel, allOrderStatusesNames);

            return orderParamsDto;
        }

        public async Task<bool> UpdateAsync(int orderId, string statusName)
        {
            var searchedOrder = await this.orders.All()
                                          .Include(x => x.OrderStatus)
                                          .FirstOrDefaultAsync(x => x.Id == orderId);

            if (searchedOrder.OrderStatus.Name == statusName)
            {
                return false;
            }

            var orderStatus = await this.orderStatusService.GetOrderStatusByName(statusName);
            searchedOrder.OrderStatus = orderStatus;

            this.orders.Update(searchedOrder);
            await this.orders.SaveAsync();

            return true;
        }
    }
}
