namespace SKAuto.Services.Mapping.PartServiceMappers
{
    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Data.Models;

    public static class PartServiceAddOutputMapper
    {
        public static PartAddOutputDtoModel Map(Part part)
        {
            var partDto = new PartAddOutputDtoModel
            {
                PartId = part.Id,
                PartName = part.Name,
                BrandAndModelName = $"{part.Brand.Name} {part.Model.Name} {part.Model.StartYear}-{part.Model.EndYear}",
                CategoryName = part.Category.Name,
                ManufactoryName = part.Manufactory == null ? "Липсва информация" : part.Manufactory.Name,
            };

            return partDto;
        }
    }
}
