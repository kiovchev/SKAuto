namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Common.DtoModels.CartDtos;
    using SKAuto.Data.Models;

    public interface IItemService
    {
        Task<Item> GetItemByPartIdAsync(int partId);

        Task<CartDtoModel> GetCartDtoByPartIdAsync(int partId);

        Task ReturnItemAsync(int itemId, int partId, int orderedQuantity);

        void UpdateItems(List<Item> items, Order order);

        Task ChangeItemAndPartQuantityInDbAsync(int itemId);

        Task<List<Item>> GetItemsByItemsIdsAsync(List<int> itemsIds);

        Task<List<Item>> GetItemsByOrderIdAsync(int orderId);

        Task<List<ItemAllDto>> GetAllItemsAsync();

        Task RemoveItemsAndAddQuantityForPartsInDbAsync();

        Task Delete(int itemId);
    }
}
