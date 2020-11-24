namespace SKAuto.Services.Data
{
    using System.Threading.Tasks;

    using SKAuto.Data.Models;

    public interface IOrderStatusService
    {
        Task<OrderStatus> GetOrderStatusByName(string name);
    }
}
