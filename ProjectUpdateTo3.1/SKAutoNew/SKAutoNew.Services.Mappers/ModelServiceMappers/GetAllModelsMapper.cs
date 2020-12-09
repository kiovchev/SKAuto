namespace SKAutoNew.Services.Mappers.ModelServiceMappers
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;

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
