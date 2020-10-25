namespace SKAuto.Services.Mapping.ModelServiceMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.ModelDtos;
    using SKAuto.Data.Models;

    public static class GetAllModelsByBrandMapper
    {
        public static IList<ModelWithImageDtoModel> Map(List<Model> allModels, string kindName)
        {
            var modelsByBrand = allModels.Select(x => new ModelWithImageDtoModel
            {
                Name = $"{kindName} {x.Name} {x.StartYear}-{x.EndYear}",
                ModelImageAddress = x.ImageAddress,
            }).ToList();

            return modelsByBrand;
        }
    }
}
