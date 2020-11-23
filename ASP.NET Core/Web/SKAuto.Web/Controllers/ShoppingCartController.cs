namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Internal;
    using SKAuto.Services.Data;
    using SKAuto.Web.HandMappers.CartMappers;
    using SKAuto.Web.Helper;
    using SKAuto.Web.ViewModels.ViewModels.CartViewModels;

    public class ShoppingCartController : Controller
    {
        private readonly IItemService itemService;

        public ShoppingCartController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(this.HttpContext.Session, "cart");

            if (cart.Count > 0)
            {
                this.ViewData["cart"] = cart;
            }

            return this.View();
        }

        public async Task<IActionResult> OrderNow(int partId)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(this.HttpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<CartViewModel>();
                var cartDto = await this.itemService.GetCartDtoByPartIdAsync(partId);
                var item = CartViewMapper.Map(cartDto);

                cart.Add(item);
                SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = cart.Select(x => x.PartId).IndexOf(partId);

                if (index == -1)
                {
                    var cartDto = await this.itemService.GetCartDtoByPartIdAsync(partId);
                    var item = CartViewMapper.Map(cartDto);
                    cart.Add(item);
                }
                else
                {
                    await this.itemService.ChangeItemAndPartQuantityInDbAsync(cart[index].ItemId);
                    cart[index].OrderedQuantity++;
                }

                SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);
            }

            return this.Redirect("/ShoppingCart/Index");
        }

        public async Task<IActionResult> Remove(int partId)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(this.HttpContext.Session, "cart");

            if (cart == null)
            {
                return this.Redirect("/ShoppingCart/Index");
            }

            int index = cart.Select(x => x.PartId).IndexOf(partId);

            if (index != -1)
            {
                var itemId = cart[index].ItemId;
                var itemPartId = cart[index].PartId;
                var quantity = cart[index].OrderedQuantity;

                await this.itemService.ReturnItemAsync(itemId, itemPartId, quantity);
                cart.RemoveAt(index);
            }

            SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);

            return this.Redirect("/ShoppingCart/Index");
        }
    }
}
