namespace SKAuto.Services.Mapping.CategoryServiceMapper
{
    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Data.Models;

    public static class GetCategoryByIdMapper
    {
        public static CategoryUpdateOutputDtoModel Map(Category category)
        {
            var currentCategory = new CategoryUpdateOutputDtoModel
            {
                Id = category.Id,
                Name = category.Name,
                ImageAddress = category.ImageAddress,
            };

            return currentCategory;
        }
    }
}
