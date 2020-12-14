namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.Common;
    using SKAutoNew.HandMappers.UseFullCategoryMapper;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.UseFullCategoryViewModels;
    using System.Threading.Tasks;

    public class UseFullCategoryController : BaseController
    {
        private readonly IUseFullCategoryService useFullCategoryService;

        public UseFullCategoryController(IUseFullCategoryService useFullCategoryService)
        {
            this.useFullCategoryService = useFullCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var dtoModels = await this.useFullCategoryService.GetAllCategoriesForIndexAsync();
            var viewModels = UseFullIndexHandMapper.Map(dtoModels);

            return this.View(viewModels);
        }

        public async Task<IActionResult> All()
        {
            var useFullCategoriesDto = await this.useFullCategoryService.GetAllUseFullCategoriesWithParamsAsync();
            var useFullCategories = UseFullCategoryAllHandMapper.Map(useFullCategoriesDto);

            return this.View(useFullCategories);
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
        public async Task<IActionResult> Create(UseFullCategoryWithImageViewModel model)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (this.ModelState.IsValid)
            {
                bool useFullCategoryExists = await this.useFullCategoryService.CheckIfExistsAsync(model.Name);

                if (useFullCategoryExists)
                {
                    var existError = new UseFullCategoryError
                    {
                        ErrorMessage = GlobalConstants.UseFullCategoryExistErrorMessage
                    };
                    return this.RedirectToAction("Error", "UseFullCategory", existError);
                }

                    await this.useFullCategoryService.CreateUseFullCategoryByNameAsync(model.Name, model.ImageAddress);

                return this.Redirect("/UseFullCategory/All");
            }

            var error = new UseFullCategoryError
            {
                ErrorMessage = GlobalConstants.UseFullCategoryModelValidationMessаge
            };
            return this.RedirectToAction("Error", "UseFullCategory", error);
        }

        public async Task<IActionResult> Update(UseFullUpdateGetIputViewModel inputViewModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new UseFullCategoryError
                {
                    ErrorMessage = GlobalConstants.UseFullCategoryModelValidationMessаge
                };
                return this.RedirectToAction("Error", "UseFullCategory", error);
            }

            var dtoInputModel = UseFullUpdateGetInputMapper.Map(inputViewModel);
            var dtoOutPutModel = await this.useFullCategoryService.GetDtoModelForUpdateOutputModelAsync(dtoInputModel);

            if (dtoOutPutModel == null)
            {
                var error = new UseFullCategoryError
                {
                    ErrorMessage = GlobalConstants.UseFullCategoryUpdateErrorMessage
                };
                return this.RedirectToAction("Error", "UseFullCategory", error);
            }

            var viewModel = UseFullUpdateGetOutPutMapper.Map(dtoOutPutModel);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UseFullUpdatePostInputViewModel viewModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new UseFullCategoryError
                {
                    ErrorMessage = GlobalConstants.UseFullCategoryModelValidationMessаge
                };
                return this.RedirectToAction("Error", "UseFullCategory", error);
            }

            var inputDtomodel = UseFullUpdatePostInputMapper.Map(viewModel);
            var isSame = await this.useFullCategoryService.UpdateUseFullCategoryAsync(inputDtomodel);

            if (isSame)
            {
                var error = new UseFullCategoryError
                {
                    ErrorMessage = GlobalConstants.UseFullCategoryExistErrorMessage
                };
                return this.RedirectToAction("Error", "UseFullCategory", error);
            }

            return this.Redirect("/UseFullCategory/Index");
        }

        public async Task<IActionResult> Delete(UseFullDeleteInputViewModel viewModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                var error = new UseFullCategoryError
                {
                    ErrorMessage = GlobalConstants.UseFullCategoryModelValidationMessаge
                };
                return this.RedirectToAction("Error", "UseFullCategory", error);
            }

            var dtoModel = UseFullCategoryDeleteHandMapper.Map(viewModel);
            var ifExists = await this.useFullCategoryService.DeleteUseFullCategoryAsync(dtoModel);

            if (ifExists)
            {
                var error = new UseFullCategoryError
                {
                    ErrorMessage = GlobalConstants.UseFullCategoryExistErrorMessage
                };
                return this.RedirectToAction("Error", "UseFullCategory", error);
            }

            return this.Redirect("/UseFullCategory/Index");
        }

        public IActionResult Error(UseFullCategoryError error)
        {
            return this.View(error);
        }
    }
}
