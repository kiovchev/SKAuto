namespace SKAutoNew.Services.Mappers.PartServiceMappers
{
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;

    public static class PartServiceGetUpdateModelMapper
    {
        public static PartUpdateOutputDtoModel Map(Part part, IList<string> brandsAndModelsNames, IList<string> categoriesNames)
        {
            var partDto = new PartUpdateOutputDtoModel
            {
                PartId = part.Id,
                PartName = part.Name,
                BrandAndModelName = $"{part.Model.Brand.Name} {part.Model.Name} {part.Model.StartYear}-{part.Model.EndYear}",
                CategoryName = part.Category.Name,
                ManufactoryName = part.Manufactory == null ? GlobalConstants.ManufactoryName : part.Manufactory.Name,
                Price = part.InComePrice,
                Quantity = part.Quantity,
                AllBrandsAndModelsNames = brandsAndModelsNames,
                AllCategoriesNames = categoriesNames,
            };

            return partDto;
        }
    }
}
