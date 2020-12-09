namespace SKAutoNew.Services.Mappers.ModelServiceMappers
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Data.Models;

    public static class ModelServiceUpdateMapper
    {
        public static Model Map(ModelUpdateInputDtoModel model, Brand brand)
        {
            var modelForUpdate = new Model()
            {
                Id = model.Id,
                Name = model.Name,
                StartYear = model.StartYear,
                EndYear = model.EndYear,
                ImageAddress = model.ImageAddress,
                Brand = brand,
            };

            return modelForUpdate;
        }
    }
}
