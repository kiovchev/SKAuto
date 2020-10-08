namespace SKAuto.Web.HandMappers.BrandMappers
{
    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Web.ViewModels.ViewModels.BrandViewModels;

    public static class BrandCreateMapper
    {
        public static BrandCreateDtoModel Map(BrandCreateInputModel brandCreateInputModel)
        {
            var brandDtoModel = new BrandCreateDtoModel()
            {
                Name = brandCreateInputModel.Name.ToUpper(),
                ImageAddress = brandCreateInputModel.ImageAddress,
            };

            return brandDtoModel;
        }
    }
}
