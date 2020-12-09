namespace SKAutoNew.Services.Mappers.PartServiceMappers
{
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class PartServiceIndexMapper
    {
        public static IList<PartGetAllPartsForIndexDtoModel> Map(List<Part> parts)
        {
            var allParts = parts.Select(x => new PartGetAllPartsForIndexDtoModel
            {
                PartId = x.Id,
                PartName = x.Name,
                BrandAndModelName = $"{x.Model.Brand.Name} {x.Model.Name} {x.Model.StartYear}-{x.Model.EndYear}",
                CategoryName = x.Category.Name,
                ManufactoryName = x.Manufactory == null ? GlobalConstants.ManufactoryName : x.Manufactory.Name,
                Quantity = x.Quantity,
                IncomePrice = x.InComePrice,
            }).ToList();

            return allParts;
        }
    }
}
