namespace SKAutoNew.HandMappers.PartMappers
{
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Web.ViewModels.PartViewModels;

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
