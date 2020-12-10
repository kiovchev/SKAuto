namespace SKAutoNew.HandMappers.TownMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Web.ViewModels.TownViewModels;

    public static class AllTownsViewModelMapper
    {
        public static AllTownsViewModel Map(AllTownsViewDtoModel dtoModel)
        {
            var model = new AllTownsViewModel
            {
                TownsNames = dtoModel.TownsNames
            };

            return model;
        }
    }
}
