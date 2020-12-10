namespace SKAutoNew.Services.Mappers.CompanyServiceMappers
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class GetCompaniesByTownAndCategoryServicemapper
    {
        public static List<CompanyInputViewDtoModel> Map(IQueryable<Company> neededCompanies, 
                                                         string townName,
                                                         string categoryName)
        {
            var companiesByTown = neededCompanies.Select(x => new CompanyInputViewDtoModel
            {
                Name = x.Name,
                TownName = townName,
                Address = x.Address,
                Phone = x.Phone,
                CategoryName = categoryName,
            }).ToList();

            return companiesByTown;
        }
    }
}
