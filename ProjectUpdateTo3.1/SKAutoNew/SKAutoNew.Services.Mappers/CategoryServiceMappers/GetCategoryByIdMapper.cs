namespace SKAutoNew.Services.Mappers.CategoryServiceMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Data.Models;

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
