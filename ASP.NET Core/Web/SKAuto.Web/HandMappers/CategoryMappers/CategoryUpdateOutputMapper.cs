namespace SKAuto.Web.HandMappers.CategoryMappers
{
    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Web.ViewModels.ViewModels.CategoryViewModels;

    public static class CategoryUpdateOutputMapper
    {
        public static CategoryUpdateOutputViewModel Map(CategoryUpdateOutputDtoModel category)
        {
            var categoryModel = new CategoryUpdateOutputViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ImageAddress = category.ImageAddress,
            };

            return categoryModel;
        }
    }
}
