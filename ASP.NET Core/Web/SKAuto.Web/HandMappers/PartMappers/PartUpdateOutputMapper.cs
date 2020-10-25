namespace SKAuto.Web.HandMappers.PartMappers
{
    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public static class PartUpdateOutputMapper
    {
        public static PartUpdateOutputModel Map(PartUpdateOutputDtoModel partDto)
        {
            var part = new PartUpdateOutputModel
            {
                PartId = partDto.PartId,
                PartName = partDto.PartName,
                BrandAndModelName = partDto.BrandAndModelName,
                CategoryName = partDto.CategoryName,
                ManufactoryName = partDto.ManufactoryName,
                Quantity = partDto.Quantity,
                Price = partDto.Price,
                AllBrandsAndModelsNames = partDto.AllBrandsAndModelsNames,
                AllCategoriesNames = partDto.AllCategoriesNames,
            };

            return part;
        }
    }
}
