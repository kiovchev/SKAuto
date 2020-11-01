namespace SKAuto.Services.Mapping.RecipientServiceMappers
{
    using SKAuto.Common.DtoModels.RecipientDtos;
    using SKAuto.Data.Models;

    public static class RecipientServiceFindOutputMapper
    {
        public static RecipientFindOutPutDtoModel Map(Part part)
        {
            var dtoModel = new RecipientFindOutPutDtoModel
            {
                PartId = part.Id,
                PartName = part.Name,
                BrandAndModelName = $"{part.Brand.Name} {part.Model.Name} {part.Model.StartYear}-{part.Model.EndYear}",
                CategoryName = part.Category.Name,
                ManufactoryName = part.Manufactory == null ? "Липсва информация" : part.Manufactory.Name,
                Price = part.CustomerPrice,
                Quantity = part.Quantity,
            };

            return dtoModel;
        }
    }
}
