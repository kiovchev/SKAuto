namespace SKAutoNew.Services.Mappers.BrandServiceMappers
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Data.Models;

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
