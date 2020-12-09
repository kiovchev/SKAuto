namespace SKAutoNew.Services.Mappers.CartServiceMappers
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Data.Models;

    public static class CartServiceMapper
    {
        public static CartDtoModel Map(Item item)
        {
            var model = new CartDtoModel
            {
                ItemId = item.ItemId,
                PartId = item.PartId,
                PartName = item.Part.Name,
                BrandAndModel = $"{item.Part.Model.Brand.Name} {item.Part.Model.Name} {item.Part.Model.StartYear}-{item.Part.Model.EndYear}",
                CustomerPrice = item.Part.CustomerPrice,
                OrderedQuantity = item.OrderedQuantity,
            };

            return model;
        }
    }
}

