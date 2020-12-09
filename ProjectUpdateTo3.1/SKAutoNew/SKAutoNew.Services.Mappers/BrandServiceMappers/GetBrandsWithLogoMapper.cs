namespace SKAutoNew.Services.Mappers.BrandServiceMappers
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

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
