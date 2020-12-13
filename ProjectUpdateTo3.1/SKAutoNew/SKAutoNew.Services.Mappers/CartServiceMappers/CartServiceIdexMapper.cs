namespace SKAutoNew.Services.Mappers.CartServiceMappers
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class CartServiceIdexMapper
    {
        public static List<ItemAllDto> Map(List<Item> itemsAll)
        {
            string addStatus;
            string addRecipient;
            var itemsDtos = new List<ItemAllDto>();

            for (int i = 0; i < itemsAll.Count; i++)
            {
                if (itemsAll[i].Order != null)
                {
                    addStatus = itemsAll[i].Order.OrderStatus.Name;
                    addRecipient = $"{itemsAll[i].Order.Recipient.FirstName} {itemsAll[i].Order.Recipient.LastName}";
                }
                else
                {
                    addStatus = "липсва статус";
                    addRecipient = "липсва получател";
                }

                var currentItem = new ItemAllDto
                {
                    ItemId = itemsAll[i].ItemId,
                    PartName = itemsAll[i].Part.Name,
                    BrandAndModelName = $"{itemsAll[i].Part.Model.Brand.Name} {itemsAll[i].Part.Model.Name} {itemsAll[i].Part.Model.StartYear}-{itemsAll[i].Part.Model.EndYear}",
                    OrderedQuantity = itemsAll[i].OrderedQuantity,
                    CustomerPrice = itemsAll[i].Part.CustomerPrice,
                    OrderStatus = addStatus,
                    OrderedAt = itemsAll[i].OrderedAt,
                    RecipientFullName = addRecipient,
                };

                itemsDtos.Add(currentItem);
            }

            itemsDtos = itemsDtos.OrderBy(x => x.OrderedAt).ThenBy(x => x.RecipientFullName).ToList();

            return itemsDtos;
        }
    }
}
