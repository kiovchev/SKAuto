namespace SKAutoNew.Services.Mappers.CategoryServiceMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class GetAllCategoriesMapper
    {
        public static IList<CategoryIndexDtoModel> Map(List<Category> categoriesAll)
        {
            var categories = categoriesAll.Select(x => new CategoryIndexDtoModel
            {
                CategoryId = x.Id,
                CategoryName = x.Name,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return categories;
        }
    }
}
