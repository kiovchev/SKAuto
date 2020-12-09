namespace SKAutoNew.HandMappers.BrandMappers
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Web.ViewModels.BrandViewModels;
    using System;

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
