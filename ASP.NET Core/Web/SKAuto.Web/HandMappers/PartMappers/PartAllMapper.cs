namespace SKAuto.Web.HandMappers.PartMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public static class PartAllMapper
    {
        public static IList<PartByCategoryAndModelViewModel> Map(IList<PartAllDtoModel> partAllDtoModels)
        {
            var allParts = partAllDtoModels.Select(x => new PartByCategoryAndModelViewModel
            {
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
