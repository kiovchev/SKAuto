namespace SKAuto.Web.HandMappers.BrandMappers
{
    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Web.ViewModels.ViewModels.BrandViewModels;

    public static class BrandUpdateGetMapper
    {
        public static BrandUpdateOutputModel Map(BrandUpdateDtoModel currentBrand)
        {
            var brand = new BrandUpdateOutputModel()
            {
                BrandId = currentBrand.BrandId,
                BrandName = currentBrand.BrandName,
                ImageAddress = currentBrand.ImageAddress,
            };

            return brand;
        }
    }
}
