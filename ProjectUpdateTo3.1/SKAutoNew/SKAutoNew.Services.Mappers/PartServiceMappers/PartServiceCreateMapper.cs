using SKAutoNew.Common.DtoModels.PartDtos;
using SKAutoNew.Data.Models;

namespace SKAutoNew.Services.Mappers.PartServiceMappers
{
    public static class PartServiceCreateMapper
    {
        public static Part Map(PartCreateInputDtoModel partModel, Model model, Category category)
        {
            var part = new Part
            {
                Name = partModel.PartName,
                Model = model,
                Category = category,
                InComePrice = partModel.Price,
                Quantity = partModel.Quantity,
            };

            return part;
        }
    }
}
