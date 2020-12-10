namespace SKAutoNew.Services.Mappers.CompanyServiceMappers
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using System.Collections.Generic;

    public static class GetCompanyCreateParamsServiceMapper
    {
        public static CompanyCreateViewDtoModel Map(List<string> townNames, List<string> categoryNames)
        {
            var viewModel = new CompanyCreateViewDtoModel()
            {
                TownNames = townNames,
                CategoryNames = categoryNames,
            };

            return viewModel;
        }
    }
}
