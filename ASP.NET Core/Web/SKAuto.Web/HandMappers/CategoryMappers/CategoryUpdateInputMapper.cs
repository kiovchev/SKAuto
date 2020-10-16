namespace SKAuto.Web.HandMappers.CategoryMappers
{
    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Web.ViewModels.ViewModels.CategoryViewModels;

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
