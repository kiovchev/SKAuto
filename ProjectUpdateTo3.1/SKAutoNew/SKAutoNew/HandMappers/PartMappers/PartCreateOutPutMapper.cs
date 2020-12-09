namespace SKAutoNew.HandMappers.PartMappers
{
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Web.ViewModels.PartViewModels;

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
