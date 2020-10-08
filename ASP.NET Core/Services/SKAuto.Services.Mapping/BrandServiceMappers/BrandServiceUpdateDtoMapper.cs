namespace SKAuto.Services.Mapping.BrandServiceMappers
{
    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Data.Models;

    public static class BrandServiceUpdateDtoMapper
    {
        public static BrandUpdateDtoModel Map(Brand brand)
        {
            var brandUpdateDto = new BrandUpdateDtoModel
            {
                BrandId = brand.Id,
                BrandName = brand.Name,
                ImageAddress = brand.ImageAddress,
            };

            return brandUpdateDto;
        }
    }
}
