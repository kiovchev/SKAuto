namespace SKAutoNew.HandMappers.UseFullCategoryMapper
{
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using SKAutoNew.Web.ViewModels.UseFullCategoryViewModels;

    public static class UseFullUpdateGetOutPutMapper
    {
        public static UseFullUpdateGetOutputViewModel Map(UseFullUpdateGetOutputDtoModel dtoModel)
        {
            var viewModel = new UseFullUpdateGetOutputViewModel
            {
                UseFullCategoryId = dtoModel.UseFullCategoryId,
                UseFullCategoryName = dtoModel.UseFullCategoryName,
                ImageAddress = dtoModel.ImageAddress
            };

            return viewModel;
        }
    }
}
