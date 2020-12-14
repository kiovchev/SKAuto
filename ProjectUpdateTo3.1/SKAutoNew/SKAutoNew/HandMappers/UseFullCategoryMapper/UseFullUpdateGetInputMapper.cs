namespace SKAutoNew.HandMappers.UseFullCategoryMapper
{
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using SKAutoNew.Web.ViewModels.UseFullCategoryViewModels;

    public static class UseFullUpdateGetInputMapper
    {
        public static UseFullUpdateGetInputDtoModel Map(UseFullUpdateGetIputViewModel viewModel)
        {
            var dtoInputModel = new UseFullUpdateGetInputDtoModel
            {
                UseFullCategoryId = viewModel.UseFullCategoryId
            };

            return dtoInputModel;
        }
    }
}
