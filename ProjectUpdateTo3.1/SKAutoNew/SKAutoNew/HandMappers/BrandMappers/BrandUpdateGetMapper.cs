namespace SKAutoNew.HandMappers.BrandMappers
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Web.ViewModels.BrandViewModels;

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
