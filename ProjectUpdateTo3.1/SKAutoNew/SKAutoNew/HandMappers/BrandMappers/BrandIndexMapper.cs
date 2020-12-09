namespace SKAutoNew.HandMappers.BrandMappers
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Web.ViewModels.BrandViewModels;
    using System.Collections.Generic;
    using System.Linq;

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
