namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Web.ViewModels.ViewModels.OrderViewModels;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public interface IOrderService
    {
        Task<List<OrdersByIdViewModel>> TakeOrdersByRecipientIdAsync(RecipientFindInputModel findInputModel);
    }
}
