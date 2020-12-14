namespace SKAutoNew.HandMappers.TownMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Web.ViewModels.TownViewModels;

    public static class TownUpdatePostInputMapper
    {
        public static TownUpdatePostInputDtoModel Map(TownUpdatePostInputModel viewModel)
        {
            var dtoModel = new TownUpdatePostInputDtoModel
            {
                TownId = viewModel.TownId,
                TownName = viewModel.TownName
            };

            return dtoModel;
        }
    }
}
