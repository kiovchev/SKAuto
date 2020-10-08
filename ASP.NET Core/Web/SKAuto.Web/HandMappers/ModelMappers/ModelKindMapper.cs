namespace SKAuto.Web.HandMappers.ModelMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.ModelDto;
    using SKAuto.Web.ViewModels.ViewModels.ModelViewModels;

    public static class ModelKindMapper
    {
        public static IList<ModelsWithImage> Map(IList<ModelWithImageDtoModel> allModelsByBrand)
        {
            var modelsByBrand = allModelsByBrand.Select(x => new ModelsWithImage
            {
                Name = x.Name,
                ModelImageAddress = x.ModelImageAddress,
            }).ToList();

            return modelsByBrand;
        }
    }
}
