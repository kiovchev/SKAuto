namespace SKAutoNew.HandMappers.CompanyMapper
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Web.ViewModels.CompanyViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class IndexHandMapper
    {
        public static IList<CompanyIndexViewmodel> Map(IList<CompanyIndexViewDtoModel> allCompanies)
        {
            var model = allCompanies.Select(x => new CompanyIndexViewmodel
            {
                CompanyId = x.CompanyId,
                Name = x.Name,
                TownName = x.TownName,
                Address = x.Address,
                Phone = x.Phone,
                CategoryName = x.CategoryName
            }).ToList();

            return model;
        }
    }
}
