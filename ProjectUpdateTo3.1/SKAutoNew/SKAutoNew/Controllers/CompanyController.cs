namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.Common;
    using SKAutoNew.HandMappers.CompanyMapper;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.CompanyViewModels;
    using System.Threading.Tasks;

    public class CompanyController : BaseController
    {
        private readonly ICompanyService company;

        public CompanyController(ICompanyService company)
        {
            this.company = company;
        }

        public async Task<IActionResult> Index()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var allCompaniesDto = await this.company.GetAllCompaniesForIndexAsync();
            var allCompanies = IndexHandMapper.Map(allCompaniesDto);

            return this.View(allCompanies);
        }

        public async Task<IActionResult> All()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var companyNamesDto = await this.company.GetAllCompaniesAsync();
            var companyNames = AllHandMapper.Map(companyNamesDto);

            return this.View(companyNames);
        }

        public async Task<IActionResult> AllByTownAndCategory(AllByTownNadCategoryModel model)
        {
            var viewModelDto = await this.company.GetCompaniesByTownAndCategoryAsync(model.TownName, model.CategoryName);
            var viewModel = AllByTownAndCategoryHandMapper.Map(viewModelDto);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            if (!this.User.IsInRole("Administrator"))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var viewModelDto = await this.company.GetCompanyCreateParamsAsync();
            var viewModel = CreateMapper.Map(viewModelDto);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyInputViewModel companyModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new CompanyError { ErrorMessage = GlobalConstants.CompanyInvalidModelMessage };
                return this.RedirectToAction("Error", "Order", error);
            }

            bool checkIfCompanyExists = await this.company.IfCompanyExistsAsync(companyModel.Name, 
                                                                                companyModel.TownName,
                                                                                companyModel.CategoryName);

            if (checkIfCompanyExists)
            {
                var error = new CompanyError { ErrorMessage = GlobalConstants.CompanyExistsMessage };
                return this.RedirectToAction("Error", "Order", error);
            }
            
            var companyModelDto = CompanyCreateInputHandMapper.Map(companyModel);

            await this.company.CreateCompanyAsync(companyModelDto);
            return this.Redirect("/Company/All");
        }

        public async Task<IActionResult> Delete(CompanyDeleteViewModel viewModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new CompanyError { ErrorMessage = GlobalConstants.CompanyInvalidModelMessage };
                return this.RedirectToAction("Error", "Order", error);
            }            

            await this.company.DeleteAsync(viewModel.CompanyId);
            return this.Redirect("/Company/Index");
        }

        public async Task<IActionResult> Update(int companyId)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var companyDto = await this.company.GetCompanyByIdAsync(companyId);
            var companyView = CompanyUpdateOutPutHandMapper.Map(companyDto);

            return this.View(companyView);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CompanyUpdateInputViewModel inputViewModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new CompanyError { ErrorMessage = GlobalConstants.CompanyInvalidModelMessage };
                return this.RedirectToAction("Error", "Order", error);
            }

            var dtoModel = CompanyUpdateInputHandMapper.Map(inputViewModel);
            var isSame =  await this.company.UpdateCompanyAsync(dtoModel);

            if (!isSame)
            {
                var error = new CompanyError { ErrorMessage = GlobalConstants.CompanySameMessage };
                return this.RedirectToAction("Error", "Order", error);
            }

            return this.Redirect("/Company/Index");
        }

        public IActionResult Error(CompanyError companyError)
        {
            return this.View(companyError);
        }
    }
}
