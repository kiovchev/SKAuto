namespace SKAutoNew.HandMappers.UseFullCategoryMapper
{
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using SKAutoNew.Web.ViewModels.UseFullCategoryViewModels;

    public static class UseFullCategoryDeleteHandMapper
    {
        public static UseFullDeleteDtoModel Map(UseFullDeleteInputViewModel viewModel)
        {
            var dtomodel = new UseFullDeleteDtoModel
            {
                UseFullCategoryId = viewModel.UseFullCategoryId
            };

            return dtomodel;
        }
    }
}
