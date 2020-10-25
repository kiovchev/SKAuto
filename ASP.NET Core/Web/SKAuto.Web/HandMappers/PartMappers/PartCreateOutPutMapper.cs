namespace SKAuto.Web.HandMappers.PartMappers
{
    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public static class PartCreateOutPutMapper
    {
        public static PartCreateViewModel Map(PartCreateOutPutDtoModel model)
        {
            var partViewmodel = new PartCreateViewModel
            {
                BrandWithModels = model.BrandWithModels,
                Categories = model.Categories,
            };

            return partViewmodel;
        }
    }
}
