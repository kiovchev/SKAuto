namespace SKAutoNew.HandMappers.ModelMappers
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Web.ViewModels.ModelViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class ModelIndexMapper
    {
        public static IList<ModelWithBrandNameOutputModel> Map(IList<ModelWithBrandNameDtoModel> models)
        {
            var modelsAll = models.Select(x => new ModelWithBrandNameOutputModel
            {
                ModelId = x.ModelId,
                ModelName = x.ModelName,
                StartYear = x.StartYear,
                EndYear = x.EndYear,
                ImageAddress = x.ImageAddress,
                BrandName = x.BrandName,
            })
                    .OrderBy(x => x.BrandName)
                    .ThenBy(x => x.ModelName)
                    .ToList();

            return modelsAll;
        }
    }
}
