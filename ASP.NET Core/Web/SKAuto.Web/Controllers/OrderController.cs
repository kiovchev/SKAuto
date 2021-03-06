﻿namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services.Data;
    using SKAuto.Web.HandMappers.OrderMappers;
    using SKAuto.Web.Helper;
    using SKAuto.Web.ViewModels.ViewModels.CartViewModels;
    using SKAuto.Web.ViewModels.ViewModels.OrderViewModel;

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
