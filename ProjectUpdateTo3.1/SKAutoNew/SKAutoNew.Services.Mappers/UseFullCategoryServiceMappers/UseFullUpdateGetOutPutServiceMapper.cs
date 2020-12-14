using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
using SKAutoNew.Data.Models;

namespace SKAutoNew.Services.Mappers.UseFullCategoryServiceMappers
{
    public static class UseFullUpdateGetOutPutServiceMapper
    {
        public static UseFullUpdateGetOutputDtoModel Map(UseFullCategory model)
        {
            var dtoOutputModel = new UseFullUpdateGetOutputDtoModel
            {
                UseFullCategoryId = model.Id,
                UseFullCategoryName = model.Name,
                ImageAddress = model.ImageAddress
            };

            return dtoOutputModel;
        }
    }
}
