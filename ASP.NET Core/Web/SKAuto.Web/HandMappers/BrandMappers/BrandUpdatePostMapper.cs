namespace SKAuto.Web.HandMappers.BrandMappers
{
    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Web.ViewModels.ViewModels.BrandViewModels;

    public static class BrandUpdatePostMapper
    {
        public static BrandUpdateDtoModel Map(BrandUpdateInputModel model)
        {
            var brand = new BrandUpdateDtoModel()
            {
                BrandId = model.Id,
                BrandName = model.Name.ToUpper(),
                ImageAddress = model.ImageAddress,
            };

            return brand;
        }
    }
}
