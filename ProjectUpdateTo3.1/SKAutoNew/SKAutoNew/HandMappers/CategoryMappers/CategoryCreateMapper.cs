namespace SKAutoNew.HandMappers.CategoryMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Web.ViewModels.CategoryViewModels;

    public static class CategoryCreateMapper
    {
        public static CategoryCreateDtoModel Map(CategoryWithImageViewModel categoryModel)
        {
            var category = new CategoryCreateDtoModel
            {
                CategoryName = categoryModel.Name,
                ImageAddress = categoryModel.ImageAddress,
            };

            return category;
        }
    }
}
