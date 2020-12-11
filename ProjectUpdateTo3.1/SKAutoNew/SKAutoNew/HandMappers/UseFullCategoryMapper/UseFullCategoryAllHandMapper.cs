namespace SKAutoNew.HandMappers.UseFullCategoryMapper
{
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using SKAutoNew.Web.ViewModels.UseFullCategoryViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class UseFullCategoryAllHandMapper
    {
        public static List<UseFullCategoryWithImageViewModel> Map(IList<UseFullCategoryWithImageViewDtoModel> useFullCategoriesDto)
        {
            var useFullCategories = useFullCategoriesDto.Select(x => new UseFullCategoryWithImageViewModel
            {
                Name = x.Name,
                ImageAddress = x.ImageAddress
            }).ToList();

            return useFullCategories;
        }
    }
}
