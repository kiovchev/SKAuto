namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.CompanyViewModels;

    public class CompanySrvice : ICompanyService
    {
        private readonly IRepository<Company> conpanies;
        private readonly IRepository<Town> towns;
        private readonly IRepository<UseFullCategory> useFullCategories;
        private readonly ITownService townService;
        private readonly IUseFullCategoryService useFullCategoryService;

        public CompanySrvice(
            IRepository<Company> conpanies,
            IRepository<Town> towns,
            IRepository<UseFullCategory> useFullCategories,
            ITownService townService,
            IUseFullCategoryService useFullCategoryService)
        {
            this.conpanies = conpanies;
            this.towns = towns;
            this.useFullCategories = useFullCategories;
            this.townService = townService;
            this.useFullCategoryService = useFullCategoryService;
        }

        public async Task CreateCompanyAsync(CompanyInputViewModel company)
        {
            UseFullCategory useFullCategory = await this.useFullCategories.All()
                                                                          .FirstOrDefaultAsync(x => x.Name == company.CategoryName);
            Town town = await this.towns.All().FirstOrDefaultAsync(x => x.Name == company.TownName);

            Company currentCompany = new Company()
            {
                Name = company.Name,
                Town = town,
                Address = company.Address,
                Phone = company.Phone,
                UseFullCategory = useFullCategory,
            };

            await this.conpanies.AddAsync(currentCompany);
            await this.conpanies.SaveChangesAsync();
        }

        public IList<CompanyInputViewModel> GetAllCompanies()
        {
            var allCompanies = this.conpanies.All();
            var allTowns = this.townService.GetAllTowns().ToList();
            var allUseFullCategories = this.useFullCategoryService.GetAllUseFullCategories().ToList();

            IList<CompanyInputViewModel> companyNames = new List<CompanyInputViewModel>();

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

            return companyNames;
        }

        public IList<CompanyInputViewModel> GetCompaniesByTownAndCategory(string townName, string categoryName)
        {
            int townId = this.towns.All().FirstOrDefault(x => x.Name == townName).Id;
            int categoryId = this.useFullCategories.All().FirstOrDefault(x => x.Name == categoryName).Id;
            var neededCompanies = this.conpanies.All().Where(x => x.TownId == townId && x.UseFullCategoryId == categoryId);

            List<CompanyInputViewModel> companiesByTown = new List<CompanyInputViewModel>();

            foreach (var company in neededCompanies)
            {
                CompanyInputViewModel currentCompany = new CompanyInputViewModel()
                {
                    Name = company.Name,
                    TownName = townName,
                    Address = company.Address,
                    Phone = company.Phone,
                    CategoryName = categoryName,
                };

                companiesByTown.Add(currentCompany);
            }

            return companiesByTown;
        }

        public CompanyCreateViewModel GetCompanyCreateParams()
        {
            List<string> townNames = this.townService.GetAllTowns().Select(x => x.Name).ToList();
            List<string> categoryNames = this.useFullCategoryService.GetAllUseFullCategories().Select(x => x.Name).ToList();

            CompanyCreateViewModel viewModel = new CompanyCreateViewModel()
            {
                TownNames = townNames,
                CategoryNames = categoryNames,
            };

            return viewModel;
        }
    }
}
