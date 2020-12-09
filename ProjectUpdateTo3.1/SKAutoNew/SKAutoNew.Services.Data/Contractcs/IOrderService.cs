namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Common.DtoModels.OrderDtos;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CartOrderCreateDtoModel dtoModel);

        Task<LastOrderDto> GetLastOrderAsync(int orderId);
    }
}
