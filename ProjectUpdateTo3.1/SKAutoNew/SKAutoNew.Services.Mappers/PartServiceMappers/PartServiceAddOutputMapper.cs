namespace SKAutoNew.Services.Mappers.PartServiceMappers
{
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Data.Models;

    public static class PartServiceAddOutputMapper
    {
        public static PartAddOutputDtoModel Map(Part part)
        {
            var partDto = new PartAddOutputDtoModel
            {
                PartId = part.Id,
                PartName = part.Name,
                BrandAndModelName = $"{part.Model.Brand.Name} {part.Model.Name} {part.Model.StartYear}-{part.Model.EndYear}",
                CategoryName = part.Category.Name,
                ManufactoryName = part.Manufactory == null ? GlobalConstants.ManufactoryName : part.Manufactory.Name,
            };

            return partDto;
        }
    }
}
