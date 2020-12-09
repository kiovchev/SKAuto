namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult All()
        {
            return this.View();
        }

        public async Task<IActionResult> Last(LastOrderParamViewModel model)
        {
            if (model.OrderId > 0)
            {
                var lastOrder = await this.orderService.GetLastOrderAsync(model.OrderId);
                var orverViewModel = OrderLastMapper.Map(lastOrder);

                return this.View(orverViewModel);
            }

            return this.Redirect("/Home/Index");
        }

        public async Task<IActionResult> Create(int recipientId)
        {
            var items = SessionHelper.GetObjectFromJson<List<CartViewModel>>(this.HttpContext.Session, "cart");
            var orderDtoModel = OrderCreateMapper.Map(recipientId, items);
            var orderId = await this.orderService.CreateOrderAsync(orderDtoModel);
            this.HttpContext.Session.Clear();

            return this.RedirectToAction("Last", "Order", new LastOrderParamViewModel { OrderId = orderId });
        }
    }
}
