namespace SKAutoNew.Services.Mappers.PartServiceMappers
{
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Data.Models;

    public static class PartServiceUpdateMapper
    {
        public static Part Map(
                               PartUpdateInputDtoModel modelDto,
                               Part part,
                               Model model,
                               Category category,
                               Manufactory manufactory)
        {
            part.Name = modelDto.PartName;
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
