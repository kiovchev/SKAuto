namespace SKAutoNew.Services.Mappers.ModelServiceMappers
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;

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
