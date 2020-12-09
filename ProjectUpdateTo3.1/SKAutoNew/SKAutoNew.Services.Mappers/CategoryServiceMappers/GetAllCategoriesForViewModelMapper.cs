namespace SKAutoNew.Services.Mappers.CategoryServiceMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class GetAllCategoriesForViewModelMapper
    {
        public static IList<CategoryAllDtoModel> Map(List<Category> allCategories)
        {
            var categoriesWithImages = allCategories.Select(x => new CategoryAllDtoModel
            {
                CategoryName = x.Name,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return categoriesWithImages;
        }
    }
}
