namespace SKAutoNew.HandMappers.OrderMappers
{
    using SKAutoNew.Common.DtoModels.OrderDtos;
    using SKAutoNew.Web.ViewModels.CartViewModels;
    using SKAutoNew.Web.ViewModels.OrderViewModels;
    using System.Linq;

    public static class OrderLastMapper
    {
        public static LastOrderViewModel Map(LastOrderDto orderDto)
        {
            var lastOrder = new LastOrderViewModel
            {
                Id = orderDto.Id,
                IssuedOn = orderDto.IssuedOn,
                OrderStatus = orderDto.OrderStatus,
                RecipientName = orderDto.RecipientName,
                Items = orderDto.Items.Select(x => new ItemForLastOrder
                {
                    ItemId = x.ItemId,
                    BrandAndModel = x.BrandAndModel,
                    OrderedQuantity = x.OrderedQuantity,
                    PartId = x.PartId,
                    PartName = x.PartName,
                    CustomerPrice = x.CustomerPrice,
                }).ToList(),
            };

            return lastOrder;
        }
    }
}
