namespace SKAutoNew.HandMappers.ModelMappers
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Web.ViewModels.ModelViewModels;

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
