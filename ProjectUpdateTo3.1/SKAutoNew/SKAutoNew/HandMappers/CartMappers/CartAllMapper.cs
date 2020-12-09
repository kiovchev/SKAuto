namespace SKAutoNew.HandMappers.CartMappers
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Web.ViewModels.CartViewModels;
    using System.Collections.Generic;
    using System.Linq;

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
