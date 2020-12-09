namespace SKAutoNew.Services.Mappers.CategoryServiceMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class GetCategoriesByNameAndYearsMapper
    {
        public static IList<GetCategoriesByNameAndYearsDtoModel> Map(IList<Category> allCategories, string modelName)
        {
            var neededCategory = allCategories.Select(x => new GetCategoriesByNameAndYearsDtoModel
            {
                Name = x.Name,
                ImageAddress = x.ImageAddress,
                ModelName = modelName,
            }).ToList();

            return neededCategory;
        }
    }
}
