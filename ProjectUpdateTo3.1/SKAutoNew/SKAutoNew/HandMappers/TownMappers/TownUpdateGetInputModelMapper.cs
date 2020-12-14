namespace SKAutoNew.HandMappers.TownMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Web.ViewModels.TownViewModels;

    public static class TownUpdateGetInputModelMapper
    {
        public static TownUpdateGetInputDtoModel Map(TownUpdateGetInputViewModel viewModel)
        {
            var dtoModel = new TownUpdateGetInputDtoModel
            {
                TownId = viewModel.TownId
            };

            return dtoModel;
        }
    }
}
