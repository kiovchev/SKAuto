namespace SKAuto.Web.HandMappers.RecipientMappers
{
    using SKAuto.Common.DtoModels.RecipientDtos;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public static class RecipientFindOutputMapper
    {
        public static RecipientFindOutputViewModel Map(RecipientFindOutPutDtoModel dtoModel)
        {
            var model = new RecipientFindOutputViewModel
            {
                PartId = dtoModel.PartId,
                PartName = dtoModel.PartName,
                BrandAndModelName = dtoModel.BrandAndModelName,
                CategoryName = dtoModel.CategoryName,
                ManufactoryName = dtoModel.ManufactoryName,
                Price = dtoModel.Price,
                Quantity = dtoModel.Quantity,
            };

            return model;
        }
    }
}
