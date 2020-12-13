namespace SKAutoNew.Services.Mappers.CompanyServiceMappers
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;

    public static class GetCompanyByIdServiceMapper
    {
        public static CompanyUpdateOutPutDtoModel Map(Company company, IList<string> towns, IList<string> categories)
        {
            var townIndex = towns.IndexOf(company.Town.Name);
            var categoryIndex = categories.IndexOf(company.UseFullCategory.Name);

            towns.RemoveAt(townIndex);
            categories.RemoveAt(categoryIndex);

            var dtoModel = new CompanyUpdateOutPutDtoModel
            {
                CompanyId = company.Id,
                CompanyName = company.Name,
                TownName = company.Town.Name,
                Address = company.Address,
                Phone = company.Phone,
                CategoryName  = company.UseFullCategory.Name,
                Towns = towns,
                Categories = categories
            };

            return dtoModel;
        }
    }
}
