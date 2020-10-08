namespace SKAuto.Web.HandMappers.BrandMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Web.ViewModels.ViewModels;

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
