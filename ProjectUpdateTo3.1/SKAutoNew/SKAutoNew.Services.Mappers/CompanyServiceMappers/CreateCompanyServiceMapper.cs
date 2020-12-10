namespace SKAutoNew.Services.Mappers.CompanyServiceMappers
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Data.Models;

    public static class CreateCompanyServiceMapper
    {
        public static Company Map(CompanyInputViewDtoModel company, Town town, UseFullCategory useFullCategory)
        {
            var currentCompany = new Company()
            {
                Name = company.Name,
                Town = town,
                Address = company.Address,
                Phone = company.Phone,
                UseFullCategory = useFullCategory,
            };

            return currentCompany;
        }
    }
}
