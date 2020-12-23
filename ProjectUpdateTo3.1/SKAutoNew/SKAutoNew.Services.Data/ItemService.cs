namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Mappers.CartServiceMappers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// business logic for Item
    /// </summary>
    public class ItemService : IItemService
    {
        private readonly IPartService partService;
        private readonly IRepository<Item> itemsRepository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="partService"></param>
        /// <param name="itemsRepository"></param>
        public ItemService(IPartService partService, IRepository<Item> itemsRepository)
        {
            this.partService = partService;
            this.itemsRepository = itemsRepository;
        }

        /// <summary>
        /// find part by id create new item and add it in database
        /// </summary>
        /// <param name="partId"></param>
        /// <returns></returns>
        public async Task<CartDtoModel> GetCartDtoByPartIdAsync(int partId)
        {
            var part = await this.partService.GetPartForItemByPartId(partId);
            var item = new Item
            {
                Part = part,
                OrderedQuantity = 1,
            };

            await this.itemsRepository.InsertAsync(item);
            await this.itemsRepository.SaveAsync();

            var cartDto = CartServiceMapper.Map(item);

            return cartDto;
        }

        /// <summary>
        /// find part by id create new item and add it in database
        /// </summary>
        /// <param name="partId"></param>
        /// <returns></returns>
        public async Task<Item> GetItemByPartIdAsync(int partId)
        {
            var part = await this.partService.GetPartForItemByPartId(partId);
            var item = new Item
            {
                Part = part,
                OrderedQuantity = 1,
            };

            await this.itemsRepository.InsertAsync(item);
            await this.itemsRepository.SaveAsync();

            return item;
        }

        /// <summary>
        /// return part, it means to increase it quantity (plus ordered) in databse and to delete item
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="partId"></param>
        /// <param name="orderedQuantity"></param>
        /// <returns></returns>
        public async Task ReturnItemAsync(int itemId, int partId, int orderedQuantity)
        {
            await this.partService.ReturnPartFromCartAsync(partId, orderedQuantity);
            var item = await this.itemsRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.ItemId == itemId);
            this.itemsRepository.Delete(item);

            await this.itemsRepository.SaveAsync();
        }

        /// <summary>
        /// increase ordered quantity, change part quantity and update both in database 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task ChangeItemAndPartQuantityInDbAsync(int itemId)
        {
            var item = await this.itemsRepository.All()
                                                 .Include(x => x.Part)
                                                 .FirstOrDefaultAsync(x => x.ItemId == itemId);
            item.OrderedQuantity++;

            await this.partService.RemoveQuantityAsync(item.PartId);
            this.itemsRepository.Update(item);
            await this.itemsRepository.SaveAsync();
        }

        /// <summary>
        /// update item in database
        /// </summary>
        /// <param name="items"></param>
        /// <param name="order"></param>
        public void UpdateItems(List<Item> items, Order order)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Order = order;
                items[i].IsOrdered = true;
                this.itemsRepository.Update(items[i]);
            }
        }

        /// <summary>
        /// get items from database, find them by id
        /// </summary>
        /// <param name="itemsIds"></param>
        /// <returns></returns>
        public async Task<List<Item>> GetItemsByItemsIdsAsync(List<int> itemsIds)
        {
            var items = await this.itemsRepository.AllAsNoTracking().ToListAsync();

            var neededItems = new List<Item>();

            for (int i = 0; i < items.Count; i++)
            {
                for (int k = 0; k < itemsIds.Count; k++)
                {
                    if (items[i].ItemId == itemsIds[k])
                    {
                        neededItems.Add(items[i]);
                    }
                }
            }

            return neededItems;
        }

        /// <summary>
        /// get items from database for an order, find them by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<List<Item>> GetItemsByOrderIdAsync(int orderId)
        {
            var items = await this.itemsRepository.All()
                                                  .Include(x => x.Part)
                                                  .ThenInclude(x => x.Model)
                                                  .ThenInclude(x => x.Brand)
                                                  .Where(x => x.OrderId == orderId)
                                                  .ToListAsync();

            return items;
        }

        /// <summary>
        /// get all items from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<ItemAllDto>> GetAllItemsAsync()
        {
            var itemsAll = await this.itemsRepository.All()
                                                     .Include(x => x.Part)
                                                     .ThenInclude(x => x.Model)
                                                     .ThenInclude(x => x.Brand)
                                                     .Include(x => x.Order)
                                                     .ThenInclude(x => x.OrderStatus)
                                                     .Include(x => x.Order)
                                                     .ThenInclude(x => x.Recipient)
                                                     .ToListAsync();

            var itemsDtos = CartServiceIdexMapper.Map(itemsAll);
            return itemsDtos;
        }

        /// <summary>
        /// remove item and return part quantity in database
        /// </summary>
        /// <returns></returns>
        public async Task RemoveItemsAndAddQuantityForPartsInDbAsync()
        {
            var allItems = await this.itemsRepository.All()
                                                     .Include(x => x.Part)
                                                     .Where(x => (DateTime.UtcNow.AddHours(2).Minute - x.OrderedAt.Minute) > 2
                                                            && x.IsOrdered == false)
                                                     .ToListAsync();

            if (allItems.Count > 0)
            {
                for (int i = 0; i < allItems.Count; i++)
                {
                    this.itemsRepository.Delete(allItems[i]);
                    await this.partService.ReturnPartFromCartAsync(allItems[i].PartId, allItems[i].OrderedQuantity);
                }

                await this.itemsRepository.SaveAsync();
            }
        }

        /// <summary>
        /// find item by id and delete it from database
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task Delete(int itemId)
        {
            var itemToDelete = await this.itemsRepository.All()
                                                         .Include(x => x.Part)
                                                         .FirstOrDefaultAsync(x => x.ItemId == itemId);
            await this.partService.ReturnPartFromCartAsync(itemToDelete.PartId, itemToDelete.OrderedQuantity);
            this.itemsRepository.Delete(itemToDelete);
            await this.itemsRepository.SaveAsync();
        }

        /// <summary>
        /// find all items for an order by order id and delet ethem from database 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task DeleteItemsForOrderAsync(int orderId)
        {
            var neededItems = await this.itemsRepository.All()
                                                  .Include(x => x.Order)
                                                  .Include(x => x.Part)
                                                  .Where(x => x.Order.Id == orderId)
                                                  .ToListAsync();

            if (neededItems.Count > 0)
            {
                foreach (var item in neededItems)
                {
                    await this.partService.ReturnPartFromCartAsync(item.PartId, item.OrderedQuantity);
                    itemsRepository.Delete(item);
                }
            }
        }
    }
}
