namespace SKAuto.Web.HandMappers.PartMappers
{
    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public static class PartUpdateInputMapper
    {
        public static PartUpdateInputDtoModel Map(PartUpdateInputModel model)
        {
            var partDto = new PartUpdateInputDtoModel
            {
                PartId = model.PartId,
                PartName = model.PartName,
                BrandAndModelName = model.BrandAndModelName,
                CategoryName = model.CategoryName,
                ManufactoryName = model.ManufactoryName,
                Price = model.Price,
                Quantity = model.Quantity,
            };

            return partDto;
        }
    }
}
