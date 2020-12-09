namespace SKAutoNew.HandMappers.CategoryMappers
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Web.ViewModels.CategoryViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class CategoryAllMapper
    {
        public static IList<CategoryWithImageViewModel> Map(IList<CategoryAllDtoModel> allCategories)
        {
            var categoryWithImages = allCategories.Select(x => new CategoryWithImageViewModel
            {
                Name = x.CategoryName,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return categoryWithImages;
        }
    }
}
