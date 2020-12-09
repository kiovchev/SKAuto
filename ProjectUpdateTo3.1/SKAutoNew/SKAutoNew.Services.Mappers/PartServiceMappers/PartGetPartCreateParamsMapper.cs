namespace SKAutoNew.Services.Mappers.PartServiceMappers
{
    using SKAutoNew.Common.DtoModels.PartDtos;
    using System.Collections.Generic;
    using System.Linq;

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
