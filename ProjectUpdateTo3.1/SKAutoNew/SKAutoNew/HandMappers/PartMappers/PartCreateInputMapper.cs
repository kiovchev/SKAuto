namespace SKAutoNew.HandMappers.PartMappers
{
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Web.ViewModels.PartViewModels;

    public static class PartCreateInputMapper
    {
        public static PartCreateInputDtoModel Map(PartCreateInputModel inputModel)
        {
            var model = new PartCreateInputDtoModel
            {
                PartName = inputModel.PartName,
                ModelName = inputModel.BrandAndModelName,
                CategoryName = inputModel.CategoryName,
                ManufactoryName = inputModel.ManufactoryName,
                Quantity = inputModel.Quantity,
                Price = inputModel.Price,
            };

            return model;
        }
    }
}
