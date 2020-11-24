namespace SKAuto.Services.Data
{
    using System.Threading.Tasks;

    using SKAuto.Common.DtoModels.CartDtos;

    public interface IOrderService
    {
        Task<int> CreateOrderAsync(CartOrderCreateDtoModel dtoModel);
    }
}
