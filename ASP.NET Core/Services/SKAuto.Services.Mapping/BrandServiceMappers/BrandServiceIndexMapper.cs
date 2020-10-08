namespace SKAuto.Services.Mapping.BrandServiceMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Data.Models;

    public static class BrandServiceIndexMapper
    {
        public static IList<BrandIndexDtoModel> Map(List<Brand> brandsAll)
        {
            var result = new List<BrandIndexDtoModel>();

            result = brandsAll.Select(x => new BrandIndexDtoModel
            {
                BrandId = x.Id,
                BrandName = x.Name,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return result;
        }
    }
}
