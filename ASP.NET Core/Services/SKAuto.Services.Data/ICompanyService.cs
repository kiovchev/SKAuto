namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Web.ViewModels.ViewModels.CompanyViewModels;

    public interface ICompanyService
    {
        IList<CompanyInputViewModel> GetAllCompanies();

        Task CreateCompanyAsync(CompanyInputViewModel company);

        IList<CompanyInputViewModel> GetCompaniesByTownAndCategory(string townName, string categoryName);

        CompanyCreateViewModel GetCompanyCreateParams();

        bool IfCompanyExists(string name, string town, string category);
    }
}
