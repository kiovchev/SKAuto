namespace SKAuto.Services.Mapping.CartServiceMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common;
    using SKAuto.Common.DtoModels.CartDtos;
    using SKAuto.Data.Models;

    public static class CartServiceIdexMapper
    {
        public static List<ItemAllDto> Map(List<Item> itemsAll)
        {
            var itemsDtos = new List<ItemAllDto>();

            for (int i = 0; i < itemsAll.Count; i++)
            {
                var currentStatusOrderName = string.Empty;
                switch (itemsAll[i].Order.OrderStatus.Name)
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

                var currentItem = new ItemAllDto
                {
                    ItemId = itemsAll[i].ItemId,
                    PartName = itemsAll[i].Part.Name,
                    BrandAndModelName = $"{itemsAll[i].Part.Brand.Name} {itemsAll[i].Part.Model.Name} {itemsAll[i].Part.Model.StartYear}-{itemsAll[i].Part.Model.EndYear}",
                    OrderedQuantity = itemsAll[i].OrderedQuantity,
                    CustomerPrice = itemsAll[i].Part.CustomerPrice,
                    OrderStatus = currentStatusOrderName,
                    OrderedAt = itemsAll[i].OrderedAt,
                    RecipientFullName = $"{itemsAll[i].Order.Recipient.FirstName} {itemsAll[i].Order.Recipient.LastName}",
                };

                itemsDtos.Add(currentItem);
            }

            itemsDtos = itemsDtos.OrderBy(x => x.OrderedAt).ThenBy(x => x.RecipientFullName).ToList();

            return itemsDtos;
        }
    }
}
