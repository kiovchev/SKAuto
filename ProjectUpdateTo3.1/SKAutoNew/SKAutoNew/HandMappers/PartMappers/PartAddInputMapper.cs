namespace SKAutoNew.HandMappers.PartMappers
{
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Web.ViewModels.PartViewModels;

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
