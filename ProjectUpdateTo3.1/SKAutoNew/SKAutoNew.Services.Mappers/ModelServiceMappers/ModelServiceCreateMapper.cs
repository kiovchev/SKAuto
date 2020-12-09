namespace SKAutoNew.Services.Mappers.ModelServiceMappers
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Data.Models;

    public static class ModelServiceCreateMapper
    {
        public static Model Map(ModelCreateDtoModel modelToCreate, Brand brand)
        {
            var model = new Model
            {
                Name = modelToCreate.Name,
                StartYear = modelToCreate.StartYear,
                EndYear = modelToCreate.EndYear,
                BrandId = brand.Id,
                Brand = brand,
                ImageAddress = modelToCreate.ImageAddress,
            };

            return model;
        }
    }
}
