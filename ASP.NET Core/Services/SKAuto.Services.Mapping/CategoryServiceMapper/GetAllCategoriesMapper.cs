namespace SKAuto.Services.Mapping.CategoryServiceMapper
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Data.Models;

    public static class GetAllCategoriesMapper
    {
        public static IList<CategoryIndexDtoModel> Map(List<Category> categoriesAll)
        {
            var categories = categoriesAll.Select(x => new CategoryIndexDtoModel
            {
                CategoryId = x.Id,
                CategoryName = x.Name,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return categories;
        }
    }
}
