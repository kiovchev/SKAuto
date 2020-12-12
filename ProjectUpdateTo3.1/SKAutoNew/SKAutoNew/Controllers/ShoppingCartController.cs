namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.Common;
    using SKAutoNew.HandMappers.CartMappers;
    using SKAutoNew.Helper;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.CartViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartController : BaseController
    {
        private readonly IItemService itemService;

        public ShoppingCartController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult Index()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            return this.View();
        }

        public async Task<IActionResult> Home()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            await this.itemService.RemoveItemsAndAddQuantityForPartsInDbAsync();

            return this.Redirect("/ShoppingCart/Index");
        }

        public async Task<IActionResult> All()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var itemsDtos = await this.itemService.GetAllItemsAsync();
            var itemsAll = CartAllMapper.Map(itemsDtos);

            return this.View(itemsAll);
        }

        public async Task<IActionResult> Delete(int itemId)
        {
            if (itemId == 0)
            {
                return this.Redirect("/Home/Index");
            }

            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            await this.itemService.Delete(itemId);

            return this.Redirect("/ShoppingCart/All");
        }

        public IActionResult Last()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(this.HttpContext.Session, "cart");

            if (cart == null)
            {
                this.HttpContext.Session.Clear();
            }
            else if (cart.Count > 0)
            {
                this.ViewData["cart"] = cart;
            }

            return this.View();
        }

        public async Task<IActionResult> OrderNow(int partId)
        {
            if (partId == 0)
            {
                return this.Redirect("/Home/Index");
            }

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
                int index = cart.Select(x => x.PartId).ToList().IndexOf(partId);

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

            return this.Redirect("/ShoppingCart/Last");
        }

        public async Task<IActionResult> Remove(int partId)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (partId == 0)
            {
                return this.Redirect("/Home/Index");
            }

            var cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(this.HttpContext.Session, "cart");

            if (cart == null)
            {
                return this.Redirect("/ShoppingCart/Last");
            }

            int index = cart.Select(x => x.PartId).ToList().IndexOf(partId);

            if (index != -1)
            {
                var itemId = cart[index].ItemId;
                var itemPartId = cart[index].PartId;
                var quantity = cart[index].OrderedQuantity;

                await this.itemService.ReturnItemAsync(itemId, itemPartId, quantity);
                cart.RemoveAt(index);
            }

            SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);

            return this.Redirect("/ShoppingCart/Last");
        }
    }
}
