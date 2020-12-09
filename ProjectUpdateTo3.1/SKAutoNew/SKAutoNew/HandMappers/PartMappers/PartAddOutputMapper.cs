namespace SKAutoNew.HandMappers.PartMappers
{
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Web.ViewModels.PartViewModels;

    public static class PartAddOutputMapper
    {
        public static PartAddOutputModel Map(PartAddOutputDtoModel model)
        {
            var part = new PartAddOutputModel
            {
                PartId = model.PartId,
                PartName = model.PartName,
                BrandAndModelName = model.BrandAndModelName,
                CategoryName = model.CategoryName,
                ManufactoryName = model.ManufactoryName,
            };

            return part;
        }
    }
}
