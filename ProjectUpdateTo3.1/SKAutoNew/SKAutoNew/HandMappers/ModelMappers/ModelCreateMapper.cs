namespace SKAutoNew.HandMappers.ModelMappers
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Web.ViewModels.ModelViewModels;

    public static class ModelCreateMapper
    {
        public static ModelCreateDtoModel Map(ModelInputViewModel inputViewModel)
        {
            var modelToCreate = new ModelCreateDtoModel
            {
                Name = inputViewModel.Name,
                StartYear = inputViewModel.StartYear,
                EndYear = inputViewModel.EndYear,
                BrandName = inputViewModel.BrandName,
                ImageAddress = inputViewModel.ImageAddress,
            };

            return modelToCreate;
        }
    }
}
