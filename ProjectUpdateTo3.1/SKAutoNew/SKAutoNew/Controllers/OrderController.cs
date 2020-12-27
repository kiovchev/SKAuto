namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.Common;
    using SKAutoNew.HandMappers.OrderMappers;
    using SKAutoNew.Helper;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.CartViewModels;
    using SKAutoNew.Web.ViewModels.OrderViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var ordersAllDtos = await this.orderService.GetAllOrdersAsync();
            var ordersAllViewModels = OrderIndexMapper.Map(ordersAllDtos);

            return this.View(ordersAllViewModels);
        }

        public async Task<IActionResult> Last(LastOrderParamViewModel model)
        {
            if (model.OrderId > 0)
            {
                var lastOrder = await this.orderService.GetLastOrderAsync(model.OrderId);
                var orverViewModel = OrderLastMapper.Map(lastOrder);

                return this.View(orverViewModel);
            }

            var error = new OrderError { ErrorMessage = GlobalConstants.OrderLastMessage };
            return this.RedirectToAction("Error", "Order", error);
        }

        public async Task<IActionResult> Create(int recipientId)
        {
            var items = SessionHelper.GetObjectFromJson<List<CartViewModel>>(this.HttpContext.Session, "cart");
            var orderDtoModel = OrderCreateMapper.Map(recipientId, items);
            var orderId = await this.orderService.CreateOrderAsync(orderDtoModel);
            this.HttpContext.Session.Clear();

            return this.RedirectToAction("Last", "Order", new LastOrderParamViewModel { OrderId = orderId });
        }

        public async Task<IActionResult> Delete(int orderId)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            await this.orderService.DeleteOrderAsync(orderId);

            return this.Redirect("/Order/Index");
        }

        public async Task<IActionResult> Update(int orderId)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var updateOutPutDtoModel = await this.orderService.GetUpdateOrderParamsAsync(orderId);
            var updateOutPutViewModel = OrderUpdateOutPutHandMapper.Map(updateOutPutDtoModel);

            return this.View(updateOutPutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateInputOrderViewModel inputModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new OrderError { ErrorMessage = GlobalConstants.OrderModelValidationMessege };
                return this.RedirectToAction("Error", "Order", error);
            }

            var isOrderUpdated = await this.orderService.UpdateAsync(inputModel.Id, inputModel.OrderStatusName);

            if (!isOrderUpdated)
            {
                var error = new OrderError { ErrorMessage = GlobalConstants.OrderSameMessage };
                return this.RedirectToAction("Error", "Order", error);
            }

            return this.Redirect("/Order/Index");
        }

        public IActionResult Error(OrderError orderError)
        {
            return this.View(orderError);
        }
    }
}
