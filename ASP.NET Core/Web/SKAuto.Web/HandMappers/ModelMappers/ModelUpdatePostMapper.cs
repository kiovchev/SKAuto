namespace SKAuto.Web.HandMappers.ModelMappers
{
    using SKAuto.Common.DtoModels.ModelDtos;
    using SKAuto.Web.ViewModels.ViewModels.ModelViewModels;

    public static class ModelUpdatePostMapper
    {
        public static ModelUpdateInputDtoModel Map(ModelUpdateInputModel model)
        {
            var modelToUpdate = new ModelUpdateInputDtoModel()
            {
                Id = model.Id,
                Name = model.Name,
                StartYear = model.StartYear,
                EndYear = model.EndYear,
                ImageAddress = model.ImageAddress,
                BrandName = model.BrandName,
            };

            return modelToUpdate;
        }
    }
}
