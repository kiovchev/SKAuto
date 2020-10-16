namespace SKAuto.Web.HandMappers.CategoryMappers
{
    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Web.ViewModels.ViewModels.CategoryViewModels;

    public static class CategoryCreateMapper
    {
        public static CategoryCreateDtoModel Map(CategoryWithImageViewModel categoryModel)
        {
            var category = new CategoryCreateDtoModel
            {
                CategoryName = categoryModel.Name,
                ImageAddress = categoryModel.ImageAdsress,
            };

            return category;
        }
    }
}
