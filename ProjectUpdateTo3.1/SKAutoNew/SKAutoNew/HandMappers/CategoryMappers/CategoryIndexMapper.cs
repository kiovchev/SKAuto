namespace SKAutoNew.HandMappers.CategoryMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Web.ViewModels.CategoryViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class CategoryIndexMapper
    {
        public static IList<CategoryIndexViewModel> Map(IList<CategoryIndexDtoModel> categoriesFromDb)
        {
            IList<CategoryIndexViewModel> categories = categoriesFromDb.Select(x => new CategoryIndexViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return categories;
        }
    }
}
