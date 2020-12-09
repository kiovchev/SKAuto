﻿namespace SKAutoNew.HandMappers.PartMappers
{
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Web.ViewModels.PartViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class PartAllMapper
    {
        public static IList<PartByCategoryAndModelViewModel> Map(IList<PartAllDtoModel> partAllDtoModels)
        {
            var allParts = partAllDtoModels.Select(x => new PartByCategoryAndModelViewModel
            {
                PartId = x.PartId,
                PartName = x.PartName,
                BrandAndModelName = x.BrandAndModelName,
                CategoryName = x.CategoryName,
                ManufactoryName = x.ManufactoryName,
                Quantity = x.Quantity,
                SellPrice = x.SellPrice,
            }).OrderBy(x => x.PartName).ToList();

            return allParts;
        }
    }
}
