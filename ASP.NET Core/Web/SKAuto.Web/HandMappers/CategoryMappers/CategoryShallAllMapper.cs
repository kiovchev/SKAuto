namespace SKAuto.Web.HandMappers.CategoryMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Web.ViewModels.ViewModels.CategoryViewModels;

    public static class CategoryShallAllMapper
    {
        public static IList<CategoryWithModelViewModel> Map(IList<GetCategoriesByNameAndYearsDtoModel> categories)
        {
            var neededCategories = categories.Select(x => new CategoryWithModelViewModel
            {
                Name = x.Name,
                ImageAdsress = x.ImageAdsress,
                ModelName = x.ModelName,
            }).ToList();

            return neededCategories;
        }
    }
}
