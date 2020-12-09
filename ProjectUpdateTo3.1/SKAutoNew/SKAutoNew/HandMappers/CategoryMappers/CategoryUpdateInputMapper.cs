namespace SKAutoNew.HandMappers.CategoryMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Web.ViewModels.CategoryViewModels;

    public static class CategoryUpdateInputMapper
    {
        public static CategoryUpdateInputDtoModel Map(CategoryUpdateInputModel category)
        {
            var categoryDto = new CategoryUpdateInputDtoModel
            {
                Id = category.Id,
                Name = category.Name,
                ImageAddress = category.ImageAddress,
            };

            return categoryDto;
        }
    }
}
