﻿namespace SKAuto.Web.Controllers
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

        public CompanyController(ICompanyService company)
        {
            this.company = company;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult All()
        {
            List<CompanyInputViewModel> companyNames = this.company.GetAllCompanies().ToList();

            return this.View(companyNames);
        }

        public IActionResult AllByTownAndCategory(string townName, string categoryName)
        {
            var viewModel = this.company.GetCompaniesByTownAndCategory(townName, categoryName);

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            if (this.User.IsInRole("Administrator"))
            {
                CompanyCreateViewModel viewModel = this.company.GetCompanyCreateParams();

                return this.View(viewModel);
            }
            else
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyInputViewModel companyModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Company/Create");
            }

            bool checkIfCompanyExists = this.company.IfCompanyExists(companyModel.Name, companyModel.TownName, companyModel.CategoryName);

            if (checkIfCompanyExists)
            {
                // create a company error page
                return this.Redirect("/Company/Create");
            }

            await this.company.CreateCompanyAsync(companyModel);
            return this.Redirect("/Company/All");
        }
    }
}
