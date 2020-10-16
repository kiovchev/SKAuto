namespace SKAuto.Services.Mapping.CategoryServiceMapper
{
    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Data.Models;

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
