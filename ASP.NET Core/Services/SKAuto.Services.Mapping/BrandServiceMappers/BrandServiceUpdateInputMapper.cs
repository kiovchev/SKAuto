namespace SKAuto.Services.Mapping.BrandServiceMappers
{
    using SKAuto.Common;
    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Data.Models;

    public static class BrandServiceUpdateInputMapper
    {
        public static Brand Map(BrandUpdateDtoModel model)
        {
            var brand = new Brand();
            brand.Id = model.BrandId;
            brand.Name = model.BrandName;

            if (model.ImageAddress == null)
            {
                model.ImageAddress = GlobalConstants.ImageAddress;
            }

            brand.ImageAddress = model.ImageAddress;

            return brand;
        }
    }
}
