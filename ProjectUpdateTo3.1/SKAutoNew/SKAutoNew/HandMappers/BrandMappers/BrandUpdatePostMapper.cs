namespace SKAutoNew.HandMappers.BrandMappers
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Web.ViewModels.BrandViewModels;

    public static class BrandUpdatePostMapper
    {
        public static BrandUpdateDtoModel Map(BrandUpdateInputModel model)
        {
            var brand = new BrandUpdateDtoModel()
            {
                BrandId = model.BrandId,
                BrandName = model.Name.ToUpper(),
                ImageAddress = model.ImageAddress,
            };

            return brand;
        }
    }
}
