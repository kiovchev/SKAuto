namespace SKAutoNew.Services.Mappers.UseFullCategoryServiceMappers
{
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class UseFullIndexServiceMapper
    {
        public static IList<IndexDtoModel> Map(List<UseFullCategory> models)
        {
            var dtoModels = models.Select(x => new IndexDtoModel
            {
                UseFullCategoryId = x.Id,
                UseFullCategoryName = x.Name,
                ImageAddress = x.ImageAddress
            }).ToList();

            return dtoModels;
        }
    }
}
