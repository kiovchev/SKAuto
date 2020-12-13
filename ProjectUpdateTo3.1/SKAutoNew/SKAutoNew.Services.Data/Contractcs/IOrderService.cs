namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Common.DtoModels.OrderDtos;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CartOrderCreateDtoModel dtoModel);

        Task<LastOrderDto> GetLastOrderAsync(int orderId);

        Task<IList<IndexOrderDto>> GetAllOrdersAsync();

        Task DeleteOrderAsync(int orderId);

        Task<UpdateOutPutOrderDtoModel> GetUpdateOrderParamsAsync(int orderId);

        Task<bool> UpdateAsync(int orderId, string statusName);

        Task DeleteAllOrdersByRecipientIdAsync(int recipientId);
    }
}
