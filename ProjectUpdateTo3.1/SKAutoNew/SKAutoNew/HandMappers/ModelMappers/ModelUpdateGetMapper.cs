namespace SKAutoNew.HandMappers.ModelMappers
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Web.ViewModels.ModelViewModels;

    public static class ModelUpdateGetMapper
    {
        public static ModelUpdateOutputModel Map(ModelUpdateOutputDtoModel currentModel)
        {
            var model = new ModelUpdateOutputModel()
            {
                ModelId = currentModel.ModelId,
                ModelName = currentModel.ModelName,
                StartYear = currentModel.StartYear,
                EndYear = currentModel.EndYear,
                ImageAddress = currentModel.ImageAddress,
                BrandName = currentModel.BrandName,
                AllBrandNames = currentModel.AllBrandNames,
            };

            return model;
        }
    }
}
