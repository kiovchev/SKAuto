namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services.Data;
    using SKAuto.Web.ViewModels.ViewModels.CompanyViewModels;

    public class CompanyController : BaseController
    {
        private readonly ICompanyService company;
        private readonly ITownService town;
        private readonly IUseFullCategoryService useFullCategory;

        public CompanyController(
            ICompanyService company,
            ITownService town,
            IUseFullCategoryService useFullCategory)
        {
            this.company = company;
            this.town = town;
            this.useFullCategory = useFullCategory;
        }

        public IActionResult All()
        {
            var allCompanies = this.company.GetAllCompanies().ToList();
            var allTowns = this.town.GetAllTowns().ToList();
            var allUseFullCategories = this.useFullCategory.GetAllUseFullCategories().ToList();
            List<CompanyInputViewModel> companyNames = new List<CompanyInputViewModel>();

            foreach (var company in allCompanies)
            {
                string townName = allTowns.FirstOrDefault(x => x.Id == company.Town.Id).Name;
                string categoryName = allUseFullCategories.FirstOrDefault(x => x.Id == company.UseFullCategory.Id).Name;
                CompanyInputViewModel companyView = new CompanyInputViewModel()
                {
                    Name = company.Name,
                    TownName = townName,
                    Address = company.Address,
                    Phone = company.Phone,
                    CategoryName = categoryName,
                };

                companyNames.Add(companyView);
            }

            return this.View(companyNames);
        }

        public IActionResult AllByTownAndCategory(string townName, string categoryName)
        {
            var viewModel = this.company.GetCompaniesByTownAndCategory(townName, categoryName);

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            List<string> townNames = this.town.GetAllTowns().Select(x => x.Name).ToList();
            List<string> categoryNames = this.useFullCategory.GetAllUseFullCategories().Select(x => x.Name).ToList();

            CompanyCreateViewModel viewModel = new CompanyCreateViewModel()
            {
                TownNames = townNames,
                CategoryNames = categoryNames,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyInputViewModel company)
        {
            await this.company.CreateCompanyAsync(company);
            return this.Redirect("/Company/All");
        }
    }
}
