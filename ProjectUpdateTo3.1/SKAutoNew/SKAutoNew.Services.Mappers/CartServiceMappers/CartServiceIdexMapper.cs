using SKAutoNew.Common;
using SKAutoNew.Common.DtoModels.CartDtos;
using SKAutoNew.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace SKAutoNew.Services.Mappers.CartServiceMappers
{
    public static class CartServiceIdexMapper
    {
        public static List<ItemAllDto> Map(List<Item> itemsAll)
        {
            var itemsDtos = new List<ItemAllDto>();

            for (int i = 0; i < itemsAll.Count; i++)
            {
                var currentItem = new ItemAllDto
                {
                    ItemId = itemsAll[i].ItemId,
                    PartName = itemsAll[i].Part.Name,
                    BrandAndModelName = $"{itemsAll[i].Part.Model.Brand.Name} {itemsAll[i].Part.Model.Name} {itemsAll[i].Part.Model.StartYear}-{itemsAll[i].Part.Model.EndYear}",
                    OrderedQuantity = itemsAll[i].OrderedQuantity,
                    CustomerPrice = itemsAll[i].Part.CustomerPrice,
                    OrderStatus = itemsAll[i].Order.OrderStatus.Name,
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
