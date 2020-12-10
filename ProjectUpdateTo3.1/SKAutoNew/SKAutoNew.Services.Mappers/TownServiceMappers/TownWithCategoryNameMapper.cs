namespace SKAutoNew.Services.Mappers.TownServiceMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using System.Collections.Generic;

    public static class TownWithCategoryNameMapper
    {
        public static TownWithCategoryNameViewDtoModel Map(string name, List<string> allTowns)
        {
            var viewModel = new TownWithCategoryNameViewDtoModel()
            {
                CategoryName = name,
                TownNames = allTowns,
            };

            return viewModel;
        }
    }
}
