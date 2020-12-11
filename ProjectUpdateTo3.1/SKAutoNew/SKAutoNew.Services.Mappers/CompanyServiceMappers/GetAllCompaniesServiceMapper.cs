namespace SKAutoNew.Services.Mappers.CompanyServiceMappers
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class GetAllCompaniesServiceMapper
    {
        public static List<CompanyInputViewDtoModel> Map(List<Company> allCompanies,
                                                         IList<Town> allTowns,
                                                         IList<UseFullCategory> allUseFullCategories)
        {
            var companyNames = new List<CompanyInputViewDtoModel>();

            foreach (var company in allCompanies)
            {
                string townName = allTowns.FirstOrDefault(x => x.Id == company.Town.Id).Name;
                string categoryName = allUseFullCategories.FirstOrDefault(x => x.Id == company.UseFullCategory.Id).Name;
                var companyView = new CompanyInputViewDtoModel()
                {
                    Name = company.Name,
                    TownName = townName,
                    Address = company.Address,
                    Phone = company.Phone,
                    CategoryName = categoryName,
                };

                companyNames.Add(companyView);
            }

            return companyNames;
        }
    }
}
