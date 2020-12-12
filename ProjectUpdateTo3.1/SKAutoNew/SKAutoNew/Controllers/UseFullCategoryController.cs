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

        public IActionResult Index()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            return this.View();
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
                    return this.Redirect("/UseFullCategory/Create");
                }

                    await this.useFullCategoryService.CreateUseFullCategoryByNameAsync(model.Name, model.ImageAddress);

                return this.Redirect("/UseFullCategory/All");
            }

            return this.Redirect("/UseFullCategory/Create");
        }
    }
}
