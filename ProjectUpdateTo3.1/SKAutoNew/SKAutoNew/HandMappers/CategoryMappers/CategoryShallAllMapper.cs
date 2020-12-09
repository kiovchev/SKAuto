namespace SKAutoNew.HandMappers.CategoryMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Web.ViewModels.CategoryViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class CategoryShallAllMapper
    {
        public static IList<CategoryWithModelViewModel> Map(IList<GetCategoriesByNameAndYearsDtoModel> categories)
        {
            var neededCategories = categories.Select(x => new CategoryWithModelViewModel
            {
                Name = x.Name,
                ImageAddress = x.ImageAddress,
                ModelName = x.ModelName,
            }).ToList();

            return neededCategories;
        }
    }
}
