namespace SKAuto.Services.Mapping.ModelServiceMappers
{
    using System.Collections.Generic;

    using SKAuto.Common.DtoModels.ModelDtos;
    using SKAuto.Data.Models;

    public static class GetAllModelsMapper
    {
        public static IList<ModelWithBrandNameDtoModel> Map(List<Model> brandsWithModels)
        {
            var models = new List<ModelWithBrandNameDtoModel>();

            foreach (var item in brandsWithModels)
            {
                var currentModel = new ModelWithBrandNameDtoModel()
                {
                    ModelId = item.Id,
                    ModelName = item.Name,
                    StartYear = item.StartYear,
                    EndYear = item.EndYear,
                    ImageAddress = item.ImageAddress,
                    BrandName = item.Brand.Name,
                };

                models.Add(currentModel);
            }

            return models;
        }
    }
}
