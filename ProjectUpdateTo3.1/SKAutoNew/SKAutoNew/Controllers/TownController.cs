namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.Common;
    using SKAutoNew.HandMappers.TownMappers;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.TownViewModels;
    using System.Threading.Tasks;

    public class TownController : BaseController
    {
        private readonly ITownService townService;

        public TownController(ITownService townService)
        {
            this.townService = townService;
        }

        public async Task<IActionResult> Index()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var dtoModels = await this.townService.GetTownsForIndexAsync();
            var viewModels = TownIndexViewMapper.Map(dtoModels);

            return this.View(viewModels);
        }

        public async Task<IActionResult> ShowAll(TownShallAllViewModel model)
        {
            var townWithCategoryDto = await this.townService.GetTownsByCategoryNameAsync(model.CategoryName);
            var townWithCategory = TownWithCategoryNameViewModelMapper.Map(townWithCategoryDto);

            return this.View(townWithCategory);
        }

        public async Task<IActionResult> All()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var allTownsDto = await this.townService.GetTownNamesAsync();
            var allTownsModel = AllTownsViewModelMapper.Map(allTownsDto);

            return this.View(allTownsModel);
        }

        public IActionResult Create()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TownInputViewModel model)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new TownError
                {
                    ErrorMessage = GlobalConstants.TownModelValidationMessаge
                };
                return this.RedirectToAction("Error", "Town", error);
            }

            bool townExists = await this.townService.CheckIfExistsAsync(model.Name);

            if (townExists)
            {
                var error = new TownError
                {
                    ErrorMessage = GlobalConstants.TownExistErrorMessage
                };
                return this.RedirectToAction("Error", "Town", error);
            }

            await this.townService.CreateTownByNameAsync(model.Name);

            return this.Redirect("/Town/All");
        }

        public async Task<IActionResult> Update(TownUpdateGetInputViewModel viewModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new TownError
                {
                    ErrorMessage = GlobalConstants.TownModelValidationMessаge
                };
                return this.RedirectToAction("Error", "Town", error);
            }

            var dtoInputModel = TownUpdateGetInputModelMapper.Map(viewModel);
            var dtoOutPutModel = await this.townService.GetTownById(dtoInputModel);

            var viewOutputModel = TownUpdateGetOutputMapper.Map(dtoOutPutModel);

            return this.View(viewOutputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TownUpdatePostInputModel inputModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new TownError
                {
                    ErrorMessage = GlobalConstants.TownModelValidationMessаge
                };
                return this.RedirectToAction("Error", "Town", error);
            }

            var dtoModel = TownUpdatePostInputMapper.Map(inputModel);
            var isSame = await this.townService.UpdateTownAsync(dtoModel);

            if (!isSame)
            {
                var error = new TownError
                {
                    ErrorMessage = GlobalConstants.TownExistErrorMessage
                };
                return this.RedirectToAction("Error", "Town", error);
            }

            return this.Redirect("/Town/Index");
        }

        public async Task<IActionResult> Delete(TownDeleteViewModel townDelete)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new TownError
                {
                    ErrorMessage = GlobalConstants.TownModelValidationMessаge
                };
                return this.RedirectToAction("Error", "Town", error);
            }

            var dtoModel = TownDeleteHandMapper.Map(townDelete);
            var isDeleted = await this.townService.DeleteTownAsync(dtoModel);

            if (!isDeleted)
            {
                var error = new TownError
                {
                    ErrorMessage = GlobalConstants.TownDeleteErrorMessage
                };
                return this.RedirectToAction("Error", "Town", error);
            }

            return this.Redirect("/Town/Index");
        }

        public IActionResult Error(TownError error)
        {
            return this.View(error);
        }
    }
}
