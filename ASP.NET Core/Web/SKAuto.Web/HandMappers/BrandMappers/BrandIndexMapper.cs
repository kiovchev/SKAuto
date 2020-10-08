namespace SKAuto.Web.HandMappers.BrandMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Web.ViewModels.ViewModels.BrandViewModels;

    public static class BrandIndexMapper
    {
        public static IList<BrndIndexViewModel> Map(IList<BrandIndexDtoModel> brands)
        {
            var brandsAll = brands.Select(x => new BrndIndexViewModel
            {
                BrandId = x.BrandId,
                BrandName = x.BrandName,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return brandsAll;
        }
    }
}
