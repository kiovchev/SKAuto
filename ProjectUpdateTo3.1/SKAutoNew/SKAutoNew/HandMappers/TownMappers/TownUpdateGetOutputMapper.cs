namespace SKAutoNew.HandMappers.TownMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Web.ViewModels.TownViewModels;

    public static class TownUpdateGetOutputMapper
    {
        public static TownUpdateOutputViewModel Map(TownUpdateGetOutPutDto dtoModel)
        {
            var model = new TownUpdateOutputViewModel
            {
                TownId = dtoModel.TownId,
                TownName = dtoModel.TownName
            };

            return model;
        }
    }
}
