namespace SKAutoNew.HandMappers.CategoryMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Web.ViewModels.CategoryViewModels;

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
