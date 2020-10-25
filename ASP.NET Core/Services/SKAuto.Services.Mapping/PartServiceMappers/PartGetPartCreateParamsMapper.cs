namespace SKAuto.Services.Mapping.PartServiceMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.PartDtos;

    public static class PartGetPartCreateParamsMapper
    {
        public static PartCreateOutPutDtoModel Map(IList<string> brandWithModelsNames, IList<string> categoriesNames)
        {
            var partModel = new PartCreateOutPutDtoModel
            {
                BrandWithModels = brandWithModelsNames.OrderBy(x => x).ToList(),
                Categories = categoriesNames.OrderBy(x => x).ToList(),
            };

            return partModel;
        }
    }
}
