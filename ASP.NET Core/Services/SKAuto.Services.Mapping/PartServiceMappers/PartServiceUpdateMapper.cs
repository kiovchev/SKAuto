namespace SKAuto.Services.Mapping.PartServiceMappers
{
    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Data.Models;

    public static class PartServiceUpdateMapper
    {
        public static Part Map(
                               PartUpdateInputDtoModel modelDto,
                               Part part,
                               Brand brand,
                               Model model,
                               Category category,
                               Manufactory manufactory)
        {
            part.Name = modelDto.PartName;
            part.Brand = brand;
            part.Model = model;
            part.Category = category;
            part.Quantity = modelDto.Quantity;
            part.InComePrice = modelDto.Price;

            if (manufactory != null)
            {
                part.Manufactory = manufactory;
            }

            return part;
        }
    }
}
