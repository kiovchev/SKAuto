namespace SKAuto.Web.HandMappers.ModelMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.ModelDto;
    using SKAuto.Web.ViewModels.ViewModels.ModelViewModels;

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
