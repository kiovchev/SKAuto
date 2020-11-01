namespace SKAuto.Services.Mapping.PartServiceMappers
{
    using SKAuto.Common;
    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Data.Models;

    public static class PartAllServiceMapper
    {
        public static PartAllDtoModel Map(Part part, string modelName, string categoryName, string manufactoryName)
        {
            var currentPart = new PartAllDtoModel
            {
                PartId = part.Id,
                PartName = part.Name,
                BrandAndModelName = modelName,
                CategoryName = categoryName,
                ManufactoryName = manufactoryName,
                Quantity = part.Quantity,
                SellPrice = part.CustomerPrice.ToString(format: GlobalConstants.PartPriceFormat),
            };

            return currentPart;
        }
    }
}
