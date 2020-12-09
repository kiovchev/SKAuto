namespace SKAutoNew.Services.Mappers.CategoryServiceMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Data.Models;

    public static class CategoryUpdateMapper
    {
        public static Category Map(CategoryUpdateInputDtoModel categoryDto)
        {
            var category = new Category
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                ImageAddress = categoryDto.ImageAddress,
            };

            return category;
        }
    }
}
