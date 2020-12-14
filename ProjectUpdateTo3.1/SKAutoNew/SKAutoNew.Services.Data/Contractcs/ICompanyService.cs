namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanyService
    {
        Task<IList<CompanyInputViewDtoModel>> GetAllCompaniesAsync();

        Task<IList<CompanyIndexViewDtoModel>> GetAllCompaniesForIndexAsync();

        Task CreateCompanyAsync(CompanyInputViewDtoModel company);

        Task<IList<CompanyInputViewDtoModel>> GetCompaniesByTownAndCategoryAsync(string townName, string categoryName);

        Task<CompanyCreateViewDtoModel> GetCompanyCreateParamsAsync();

        Task<bool> IfCompanyExistsAsync(string name, string town, string category);

        Task<bool> DeleteAsync(int companyId);

        Task<CompanyUpdateOutPutDtoModel> GetCompanyByIdAsync(int companyId);

        Task<bool> UpdateCompanyAsync(CompanyUpdateInputDtoModel dtoModel);
    }
}
