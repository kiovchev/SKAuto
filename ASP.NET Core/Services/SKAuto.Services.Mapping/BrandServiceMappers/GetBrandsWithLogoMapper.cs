namespace SKAuto.Services.Mapping.BrandServiceMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Data.Models;

    public static class GetBrandsWithLogoMapper
    {
        public static IList<BrandWithLogoDtoModel> Map(List<Brand> allBrands)
        {
            var brandsWithLogos = allBrands.Select(x => new BrandWithLogoDtoModel
            {
                BrandName = x.Name,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return brandsWithLogos;
        }
    }
}
