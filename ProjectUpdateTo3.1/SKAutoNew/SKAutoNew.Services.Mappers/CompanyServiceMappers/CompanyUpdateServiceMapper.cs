namespace SKAutoNew.Services.Mappers.CompanyServiceMappers
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Data.Models;

    public static class CompanyUpdateServiceMapper
    {
        public static Company Map(Company companyForCange, 
                                  CompanyUpdateInputDtoModel dtoModel,
                                  Town neededTown,
                                  UseFullCategory neededCategory)
        {
            companyForCange.Name = dtoModel.CompanyName;
            companyForCange.Town = neededTown;
            companyForCange.Address = dtoModel.Address;
            companyForCange.Phone = dtoModel.Phone;
            companyForCange.UseFullCategory = neededCategory;

            return companyForCange;
        }
    }
}
