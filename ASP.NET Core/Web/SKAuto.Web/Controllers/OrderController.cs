namespace SKAuto.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services.Data;
    using SKAuto.Web.ViewModels.ViewModels.OrderViewModels;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public class OrderController : Controller
    {
        private readonly IRecipientService recipientService;
        private readonly IOrderService orderService;

        public OrderController(IRecipientService recipientService, IOrderService orderService)
        {
            this.recipientService = recipientService;
            this.orderService = orderService;
        }

        public IActionResult AllById()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AllById(RecipientFindInputModel findInputModel)
        {
            bool ifExists = await this.recipientService.IfRecipientExistsAsync(findInputModel.Phone);

            if (!this.ModelState.IsValid || ifExists)
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var ordersAll = await this.orderService.TakeOrdersByRecipientIdAsync(findInputModel);

            return this.RedirectToAction("ShowAll", "Order", ordersAll);
        }

        public IActionResult ShowAll(OrdersByIdViewModel ordersByIdViewModel)
        {
            if (this.ModelState.IsValid)
            {
                // TODO need to make a view
                return this.View();
            }
            else
            {
                return this.Redirect("/Order/AllById");
            }
        }

        public IActionResult CreateOrAdd(RecipientOrderIpnutModel ipnutModel)
        {
            // TODO write method in OrderService
            return null;
        }
    }
}
