namespace SKAutoNew.Services.Mappers.BrandServiceMappers
{
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Data.Models;

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
