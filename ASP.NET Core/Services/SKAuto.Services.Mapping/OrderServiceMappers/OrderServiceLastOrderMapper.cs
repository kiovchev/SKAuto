namespace SKAuto.Services.Mapping.OrderServiceMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common;
    using SKAuto.Common.DtoModels.CartDtos;
    using SKAuto.Common.DtoModels.OrderDtos;
    using SKAuto.Data.Models;

    public static class OrderServiceLastOrderMapper
    {
        public static LastOrderDto Map(Order lastOrder, List<Item> orderItems)
        {
            var currentStatusOrderName = string.Empty;
            switch (lastOrder.OrderStatus.Name)
            {
                case GlobalConstants.PendingStatus:
                    currentStatusOrderName = GlobalConstants.PendingBGStatus;
                    break;
                case GlobalConstants.ShippedStatus:
                    currentStatusOrderName = GlobalConstants.ShippedBGStatus;
                    break;
                case GlobalConstants.DeliverStatus:
                    currentStatusOrderName = GlobalConstants.DeliverBGStatus;
                    break;
                case GlobalConstants.AcquiredStatus:
                    currentStatusOrderName = GlobalConstants.AcquiredBGStatus;
                    break;

                default:
                    break;
            }

            var neededOrder = new LastOrderDto
            {
                Id = lastOrder.Id,
                IssuedOn = lastOrder.IssuedOn,
                RecipientName = $"{lastOrder.Recipient.FirstName} {lastOrder.Recipient.LastName}",
                Items = orderItems.Select(x => new ItemForLastOrderDto
                {
                    ItemId = x.ItemId,
                    BrandAndModel = $"{x.Part.Brand.Name} {x.Part.Model.Name} {x.Part.Model.StartYear}-{x.Part.Model.EndYear}",
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
