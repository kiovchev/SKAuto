using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
using SKAutoNew.Data.Models;

namespace SKAutoNew.Services.Mappers.UseFullCategoryServiceMappers
{
    public static class UseFullUpdatePostIputServiceMapper
    {
        public static UseFullCategory Map(UseFullCategory modelToUpdate, UseFullUpdatePostInputDtoModel dtoModel)
        {
            modelToUpdate.ImageAddress = dtoModel.ImageAddress;
            modelToUpdate.Name = dtoModel.UseFullCategoryName;

            return modelToUpdate;
        }
    }
}
