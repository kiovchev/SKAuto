namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Mappers.CompanyServiceMappers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompanySrvice : ICompanyService
    {
        private readonly IRepository<Company> conpanies;
        private readonly ITownService townService;
        private readonly IUseFullCategoryService useFullCategoryService;

        public CompanySrvice(
            IRepository<Company> conpanies,
            ITownService townService,
            IUseFullCategoryService useFullCategoryService)
        {
            this.conpanies = conpanies;
            this.townService = townService;
            this.useFullCategoryService = useFullCategoryService;
        }

        public async Task CreateCompanyAsync(CompanyInputViewDtoModel company)
        {
            var useFullCategories = await this.useFullCategoryService.GetAlluseFullCategoriesAsync();
            var useFullCategory = useFullCategories.FirstOrDefault(x => x.Name == company.CategoryName);

            var allTowns = await this.townService.GetAllTownsAsync();
            var town = allTowns.FirstOrDefault(x => x.Name == company.TownName);
            
            var currentCompany = CreateCompanyServiceMapper.Map(company, town, useFullCategory);

            await this.conpanies.InsertAsync(currentCompany);
            await this.conpanies.SaveAsync();
        }

        public async Task<bool> DeleteAsync(int companyId)
        {
            var currentCompay = await this.conpanies.All().FirstOrDefaultAsync(x => x.Id == companyId);

            if (currentCompay != null)
            {
                this.conpanies.Delete(currentCompay);
                await this.conpanies.SaveAsync();

                return true;
            }

            return false;
        }

        public async Task<IList<CompanyInputViewDtoModel>> GetAllCompaniesAsync()
        {
            var allCompanies = await this.conpanies.All()
                                             .Include(x => x.Town)
                                             .Include(x => x.UseFullCategory)
                                             .ToListAsync();
            var allTowns = await this.townService.GetAllTownsAsync();
            var allUseFullCategories = await this.useFullCategoryService.GetAlluseFullCategoriesAsync();

            var companyNames = GetAllCompaniesServiceMapper.Map(allCompanies, allTowns, allUseFullCategories);

            return companyNames;
        }

        public async Task<IList<CompanyIndexViewDtoModel>> GetAllCompaniesForIndexAsync()
        {
            var allCompanies = await this.conpanies.All()
                                             .Include(x => x.Town)
                                             .Include(x => x.UseFullCategory)
                                             .ToListAsync();
            var allTowns = await this.townService.GetAllTownsAsync();
            var allUseFullCategories = await this.useFullCategoryService.GetAlluseFullCategoriesAsync();

            var allCompaniesModel = GetAllCompaniesForIndexServiceMapper.Map(allCompanies, allTowns, allUseFullCategories)
                                                           .OrderBy(x => x.TownName)
                                                           .ThenBy(x => x.CategoryName)
                                                           .ToList();

            return allCompaniesModel;
        }

        public async Task<IList<CompanyInputViewDtoModel>> GetCompaniesByTownAndCategoryAsync(string townName,
                                                                                           string categoryName)
        {
            var allTowns = await this.townService.GetAllTownsAsync();
            var allCategories = await this.useFullCategoryService.GetAlluseFullCategoriesAsync();

            int townId = allTowns.FirstOrDefault(x => x.Name == townName).Id;
            int categoryId = allCategories.FirstOrDefault(x => x.Name == categoryName).Id;

            var neededCompanies = this.conpanies.All()
                                                .Where(x => x.TownId == townId
                                                && x.UseFullCategoryId == categoryId);

            var companiesByTown = GetCompaniesByTownAndCategoryServicemapper.Map(neededCompanies, townName, categoryName);

            return companiesByTown;
        }

        public async Task<CompanyUpdateOutPutDtoModel> GetCompanyByIdAsync(int companyId)
        {
            var allTowns = await this.townService.GetAllTownsAsync();
            var allCategories = await this.useFullCategoryService.GetAlluseFullCategoriesAsync();

            var townNames = allTowns.Select(x => x.Name).OrderBy(x => x).ToList();
            var categoryNames = allCategories.Select(x => x.Name).OrderBy(x => x).ToList();

            var neededCompany = await this.conpanies.All()
                                             .Include(x => x.Town)
                                             .Include(x => x.UseFullCategory)
                                             .FirstOrDefaultAsync(x => x.Id == companyId);

            var companyDto = GetCompanyByIdServiceMapper.Map(neededCompany, townNames, categoryNames);

            return companyDto;
        }

        public async Task<CompanyCreateViewDtoModel> GetCompanyCreateParamsAsync()
        {
            var allTowns = await this.townService.GetAllTownsAsync();
            var allCategories = await this.useFullCategoryService.GetAlluseFullCategoriesAsync();

            var townNames = allTowns.Select(x => x.Name).OrderBy(x => x).ToList();
            var categoryNames = allCategories.Select(x => x.Name).OrderBy(x => x).ToList();

            var viewModel = GetCompanyCreateParamsServiceMapper.Map(townNames, categoryNames);

            return viewModel;
        }

        public async Task<bool> IfCompanyExistsAsync(string name, string town, string category)
        {
            var hasCompany = await this.conpanies.All()
                                                 .AnyAsync(x => x.Name.ToUpper() == name.ToUpper()
                                                           && x.Town.Name.ToUpper() == town.ToUpper() 
                                                           && x.UseFullCategory.Name.ToUpper() == category.ToUpper());

            return hasCompany;
        }

        public async Task<bool> UpdateCompanyAsync(CompanyUpdateInputDtoModel dtoModel)
        {
            var allCompanies = await this.conpanies.All()
                                                   .Include(x => x.Town)
                                                   .Include(x => x.UseFullCategory)
                                                   .ToListAsync();

            if (allCompanies.Any(x => x.Name == dtoModel.CompanyName
                && x.Town.Name == dtoModel.TownName
                && x.Address == dtoModel.Address
                && x.Phone == dtoModel.Phone
                && x.UseFullCategory.Name == dtoModel.CategoryName))
            {
                return false;
            }

            var companyToChange = allCompanies.FirstOrDefault(x => x.Id == dtoModel.CompanyId);

            

            var allTowns = await this.townService.GetAllTownsAsync();
            var neededTown = allTowns.FirstOrDefault(x => x.Name == dtoModel.TownName);

            var allCategories = await this.useFullCategoryService.GetAlluseFullCategoriesAsync();
            var neededCategory = allCategories.FirstOrDefault(x => x.Name == dtoModel.CategoryName);

            companyToChange = CompanyUpdateServiceMapper.Map(companyToChange, dtoModel, neededTown, neededCategory);

            this.conpanies.Update(companyToChange);
            await this.conpanies.SaveAsync();

            return true;
        }
    }
}
