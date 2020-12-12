namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderStatusService
    {
        Task<OrderStatus> GetOrderStatusByName(string name);

        Task<IList<string>> GetAllOrderStatusesNamesAsync();
    }
}
