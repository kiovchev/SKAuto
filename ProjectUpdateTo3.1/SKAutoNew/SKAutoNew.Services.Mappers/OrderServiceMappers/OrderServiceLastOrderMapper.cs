namespace SKAutoNew.Services.Mappers.OrderServiceMappers
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Common.DtoModels.OrderDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class OrderServiceLastOrderMapper
    {
        public static LastOrderDto Map(Order lastOrder, List<Item> orderItems)
        {
            var neededOrder = new LastOrderDto
            {
                Id = lastOrder.Id,
                IssuedOn = lastOrder.IssuedOn,
                RecipientName = $"{lastOrder.Recipient.FirstName} {lastOrder.Recipient.LastName}",
                Items = orderItems.Select(x => new ItemForLastOrderDto
                {
                    ItemId = x.ItemId,
                    BrandAndModel = $"{x.Part.Model.Brand.Name} {x.Part.Model.Name} {x.Part.Model.StartYear}-{x.Part.Model.EndYear}",
                    OrderedQuantity = x.OrderedQuantity,
                    CustomerPrice = x.Part.CustomerPrice,
                    PartName = x.Part.Name,
                }).ToList(),
                OrderStatus = lastOrder.OrderStatus.Name,
            };

            return neededOrder;
        }
    }
}
