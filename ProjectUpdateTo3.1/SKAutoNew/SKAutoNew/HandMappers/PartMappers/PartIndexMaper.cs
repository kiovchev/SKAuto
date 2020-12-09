namespace SKAutoNew.HandMappers.PartMappers
{
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Web.ViewModels.PartViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class PartIndexMaper
    {
        public static IList<PartIndexViewModel> Map(IList<PartGetAllPartsForIndexDtoModel> partsDtoModel)
        {
            var partsAll = partsDtoModel.Select(x => new PartIndexViewModel
            {
                PartId = x.PartId,
                PartName = x.PartName,
                BrandAndModelName = x.BrandAndModelName,
                CategoryName = x.CategoryName,
                ManufactoryName = x.ManufactoryName,
                Quantity = x.Quantity,
                IncomePrice = x.IncomePrice,
            })
                .OrderBy(x => x.BrandAndModelName)
                .ThenBy(x => x.PartName)
                .ToList();

            return partsAll;
        }
    }
}
