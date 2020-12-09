namespace SKAutoNew.HandMappers.ModelMappers
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Web.ViewModels.ModelViewModels;
    using System.Collections.Generic;
    using System.Linq;

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
