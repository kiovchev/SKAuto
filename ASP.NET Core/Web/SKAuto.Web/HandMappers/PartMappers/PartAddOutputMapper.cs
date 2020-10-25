namespace SKAuto.Web.HandMappers.PartMappers
{
    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

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
