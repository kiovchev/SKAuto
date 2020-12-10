namespace SKAutoNew.HandMappers.TownMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Web.ViewModels.TownViewModels;

    public static class TownWithCategoryNameViewModelMapper
    {
        public static TownWithCategoryNameViewModel Map(TownWithCategoryNameViewDtoModel dtoModel)
        {
            var model = new TownWithCategoryNameViewModel
            {
                CategoryName = dtoModel.CategoryName,
                TownNames = dtoModel.TownNames
            };

            return model;
        }
    }
}
