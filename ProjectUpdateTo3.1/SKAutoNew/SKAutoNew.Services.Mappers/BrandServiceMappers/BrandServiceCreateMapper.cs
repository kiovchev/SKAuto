namespace SKAutoNew.Services.Mappers.BrandServiceMappers
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Data.Models;

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
