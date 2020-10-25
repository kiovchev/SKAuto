namespace SKAuto.Services.Mapping.PartServiceMappers
{
    using System.Collections.Generic;

    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Data.Models;

    public static class PartServiceGetUpdateModelMapper
    {
        public static PartUpdateOutputDtoModel Map(Part part, IList<string> brandsAndModelsNames, IList<string> categoriesNames)
        {
            var partDto = new PartUpdateOutputDtoModel
            {
                PartId = part.Id,
                PartName = part.Name,
                BrandAndModelName = $"{part.Brand.Name} {part.Model.Name} {part.Model.StartYear}-{part.Model.EndYear}",
                CategoryName = part.Category.Name,
                ManufactoryName = part.Manufactory == null ? "Липсва информация" : part.Manufactory.Name,
                Price = part.InComePrice,
                Quantity = part.Quantity,
                AllBrandsAndModelsNames = brandsAndModelsNames,
                AllCategoriesNames = categoriesNames,
            };

            return partDto;
        }
    }
}
