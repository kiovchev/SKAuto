namespace SKAutoNew.Services.Mappers.ModelServiceMappers
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

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
