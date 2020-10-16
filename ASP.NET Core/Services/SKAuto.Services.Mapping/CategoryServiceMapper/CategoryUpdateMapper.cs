namespace SKAuto.Services.Mapping.CategoryServiceMapper
{
    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Data.Models;

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
