namespace SKAutoNew.HandMappers.BrandMappers
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Web.ViewModels.BrandViewModels;
    using System;

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
