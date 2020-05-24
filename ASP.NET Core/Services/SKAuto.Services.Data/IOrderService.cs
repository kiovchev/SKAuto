namespace SKAuto.Services.Data
{
    using System.Threading.Tasks;

    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Recipient recipient, PartByCategoryAndModelViewModel partModel);
    }
}
