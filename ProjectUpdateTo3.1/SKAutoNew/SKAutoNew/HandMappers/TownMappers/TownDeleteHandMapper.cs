namespace SKAutoNew.HandMappers.TownMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Web.ViewModels.TownViewModels;

    public static class TownDeleteHandMapper
    {
        public static TownDeleteDtoModel Map(TownDeleteViewModel viewModel)
        {
            var dtoModel = new TownDeleteDtoModel
            {
                TownId = viewModel.TownId
            };

            return dtoModel;
        }
    }
}
