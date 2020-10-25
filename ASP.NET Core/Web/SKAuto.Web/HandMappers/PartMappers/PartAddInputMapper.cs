namespace SKAuto.Web.HandMappers.PartMappers
{
    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public static class PartAddInputMapper
    {
        public static PartAddInputDtoModel Map(PartAddInputModel model)
        {
            var partDto = new PartAddInputDtoModel
            {
                PartId = model.PartId,
                Price = model.Price,
                Quantity = model.Quantity,
            };

            return partDto;
        }
    }
}
