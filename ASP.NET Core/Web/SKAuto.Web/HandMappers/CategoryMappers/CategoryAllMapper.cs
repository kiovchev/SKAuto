namespace SKAuto.Web.HandMappers.CategoryMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Web.ViewModels.ViewModels.CategoryViewModels;

    public static class CategoryAllMapper
    {
        public static IList<CategoryWithImageViewModel> Map(IList<CategoryAllDtoModel> allCategories)
        {
            var categoryWithImages = allCategories.Select(x => new CategoryWithImageViewModel
            {
                Name = x.CategoryName,
                ImageAdsress = x.ImageAddress,
            }).ToList();

            return categoryWithImages;
        }
    }
}
