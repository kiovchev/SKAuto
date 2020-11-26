namespace SKAuto.Services.Data
{
    using System.Threading.Tasks;

    using SKAuto.Common.DtoModels.CartDtos;
    using SKAuto.Common.DtoModels.OrderDtos;

    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CartOrderCreateDtoModel dtoModel);

        Task<LastOrderDto> GetLastOrderAsync(int orderId);
    }
}
