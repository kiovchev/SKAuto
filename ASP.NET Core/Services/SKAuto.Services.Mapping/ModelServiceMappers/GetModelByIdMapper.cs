namespace SKAuto.Services.Mapping.ModelServiceMappers
{
    using System.Collections.Generic;

    using SKAuto.Common.DtoModels.ModelDto;
    using SKAuto.Data.Models;

    public static class GetModelByIdMapper
    {
        public static ModelUpdateOutputDtoModel Map(Model currentModel, IList<string> allBrandsName)
        {
            var neededModel = new ModelUpdateOutputDtoModel()
            {
                ModelId = currentModel.Id,
                ModelName = currentModel.Name,
                StartYear = currentModel.StartYear,
                EndYear = currentModel.EndYear,
                ImageAddress = currentModel.ImageAddress,
                BrandName = currentModel.Brand.Name,
                AllBrandNames = allBrandsName,
            };

            return neededModel;
        }
    }
}
