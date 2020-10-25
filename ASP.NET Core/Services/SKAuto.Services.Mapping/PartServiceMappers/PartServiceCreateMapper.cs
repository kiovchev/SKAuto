namespace SKAuto.Services.Mapping.PartServiceMappers
{
    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Data.Models;

    public static class PartServiceCreateMapper
    {
        public static Part Map(PartCreateInputDtoModel partModel, Brand brand, Model model, Category category)
        {
            var part = new Part
            {
                Name = partModel.PartName,
                Brand = brand,
                Model = model,
                Category = category,
                InComePrice = partModel.Price,
                Quantity = partModel.Quantity,
            };

            return part;
        }
    }
}
