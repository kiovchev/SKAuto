namespace SKAuto.Services.Mapping.BrandServiceMappers
{
    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Data.Models;

    public static class BrandServiceCreateMapper
    {
        public static Brand Map(BrandCreateDtoModel brandCreateDtoModel)
        {
            var brand = new Brand
            {
                Name = brandCreateDtoModel.Name,
                ImageAddress = brandCreateDtoModel.ImageAddress,
            };

            return brand;
        }
    }
}
