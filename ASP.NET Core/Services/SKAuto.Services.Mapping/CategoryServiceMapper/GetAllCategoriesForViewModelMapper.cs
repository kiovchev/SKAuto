namespace SKAuto.Services.Mapping.CategoryServiceMapper
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Data.Models;

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
