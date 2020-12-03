namespace SKAuto.Web.HandMappers.CartNappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.CartDtos;
    using SKAuto.Web.ViewModels.ViewModels.CatViewModels;

    public static class CartAllMapper
    {
        public static List<ItemAllViewModel> Map(List<ItemAllDto> itemAllDtos)
        {
            var items = itemAllDtos.Select(x => new ItemAllViewModel
            {
                ItemId = x.ItemId,
                PartName = x.PartName,
                BrandAndModelName = x.BrandAndModelName,
                OrderedQuantity = x.OrderedQuantity,
                OrderedAt = x.OrderedAt,
                CustomerPrice = x.CustomerPrice,
                OrderStatus = x.OrderStatus,
                RecipientFullName = x.RecipientFullName,
            })
                .ToList();

            return items;
        }
    }
}
