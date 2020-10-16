namespace SKAuto.Web.HandMappers.CategoryMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Web.ViewModels.ViewModels.CategoryViewModels;

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
