namespace SKAutoNew.HandMappers.UseFullCategoryMapper
{
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using SKAutoNew.Web.ViewModels.UseFullCategoryViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class UseFullIndexHandMapper
    {
        public static IList<UseFullIndexViewModel> Map(IList<IndexDtoModel> indexDtoModels)
        {
            var viewModels = indexDtoModels.Select(x => new UseFullIndexViewModel
            {
                UseFullCategoryId = x.UseFullCategoryId,
                UseFullCategoryName = x.UseFullCategoryName,
                ImageAddress = x.ImageAddress
            }).ToList();

            return viewModels;
        }
    }
}
