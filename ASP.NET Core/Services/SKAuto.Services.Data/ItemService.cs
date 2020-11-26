namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Common.DtoModels.CartDtos;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Services.Mapping.CartServiceMappers;

    public class ItemService : IItemService
    {
        private readonly IPartService partService;
        private readonly IRepository<Item> itemsRepository;

        public ItemService(IPartService partService, IRepository<Item> itemsRepository)
        {
            this.partService = partService;
            this.itemsRepository = itemsRepository;
        }

        public async Task<CartDtoModel> GetCartDtoByPartIdAsync(int partId)
        {
            var part = await this.partService.GetPartForItemByPartId(partId);
            var item = new Item
            {
                Part = part,
                OrderedQuantity = 1,
            };

            await this.itemsRepository.AddAsync(item);
            await this.itemsRepository.SaveChangesAsync();

            var cartDto = CartServiceMapper.Map(item);

            return cartDto;
        }

        public async Task<Item> GetItemByPartIdAsync(int partId)
        {
            var part = await this.partService.GetPartForItemByPartId(partId);
            var item = new Item
            {
                Part = part,
                OrderedQuantity = 1,
            };

            await this.itemsRepository.AddAsync(item);
            await this.itemsRepository.SaveChangesAsync();

            return item;
        }

        public async Task ReturnItemAsync(int itemId, int partId, int orderedQuantity)
        {
            await this.partService.ReturnPartFromCartAsync(partId, orderedQuantity);
            var item = await this.itemsRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.ItemId == itemId);
            this.itemsRepository.Delete(item);

            await this.itemsRepository.SaveChangesAsync();
        }

        public async Task ChangeItemAndPartQuantityInDbAsync(int itemId)
        {
            var item = await this.itemsRepository.All().FirstOrDefaultAsync(x => x.ItemId == itemId);
            item.OrderedQuantity++;

            // долния ред може би не ни е нужен да се провери

            // item.DbQuantity = await this.partService.GetPartQuantityAsync(item.PartId);
            this.itemsRepository.Update(item);
            await this.itemsRepository.SaveChangesAsync();
        }

        public void UpdateItems(List<Item> items, Order order)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Order = order;
                items[i].IsOrdered = true;
                this.itemsRepository.Update(items[i]);
            }
        }

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
    }
}
