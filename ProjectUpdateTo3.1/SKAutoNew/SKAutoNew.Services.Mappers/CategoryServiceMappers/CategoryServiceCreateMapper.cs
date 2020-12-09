namespace SKAutoNew.Services.Mappers.CategoryServiceMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Data.Models;

    public static class CategoryServiceCreateMapper
    {
        public static Category Map(CategoryCreateDtoModel categoryCreateDto)
        {
            Category category = new Category
            {
                Name = categoryCreateDto.CategoryName,
                ImageAddress = categoryCreateDto.ImageAddress,
            };

            return category;
        }
    }
}
