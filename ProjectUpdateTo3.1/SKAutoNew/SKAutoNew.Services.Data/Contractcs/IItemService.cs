namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
