namespace SKAuto.Services.Mapping.PartServiceMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Data.Models;

    public static class PartServiceIndexMapper
    {
        public static IList<PartGetAllPartsForIndexDtoModel> Map(List<Part> parts)
        {
            var allParts = parts.Select(x => new PartGetAllPartsForIndexDtoModel
            {
                PartId = x.Id,
                PartName = x.Name,
                BrandAndModelName = $"{x.Brand.Name} {x.Model.Name} {x.Model.StartYear}-{x.Model.EndYear}",
                CategoryName = x.Category.Name,
                ManufactoryName = x.Manufactory == null ? "Липсва информация" : x.Manufactory.Name,
                Quantity = x.Quantity,
                IncomePrice = x.InComePrice,
            }).ToList();

            return allParts;
        }
    }
}
