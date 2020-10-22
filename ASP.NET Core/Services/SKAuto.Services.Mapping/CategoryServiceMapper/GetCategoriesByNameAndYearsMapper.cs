namespace SKAuto.Services.Mapping.CategoryServiceMapper
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Data.Models;

    public static class GetCategoriesByNameAndYearsMapper
    {
        public static IList<GetCategoriesByNameAndYearsDtoModel> Map(IList<Category> allCategories, string modelName)
        {
            var neededCategory = allCategories.Select(x => new GetCategoriesByNameAndYearsDtoModel
            {
                Name = x.Name,
                ImageAdsress = x.ImageAddress,
                ModelName = modelName,
            }).ToList();

            return neededCategory;
        }
    }
}
