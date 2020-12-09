namespace SKAutoNew.HandMappers.BrandMappers
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Web.ViewModels.BrandViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class BrandAllMapper
    {
        public static IList<BrandWithLogoViewModel> Map(IList<BrandWithLogoDtoModel> brandsWithLogos)
        {
            var brands = brandsWithLogos.Select(x => new BrandWithLogoViewModel
            {
                BrandName = x.BrandName,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return brands;
        }
    }
}
