namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.CompanyViewModels;

    public interface ICompanyService
    {
        IQueryable<Company> GetAllCompanies();

        Task CreateCompanyAsync(CompanyInputViewModel company);

        IList<CompanyInputViewModel> GetCompaniesByTownAndCategory(string townName, string categoryName);
    }
}
