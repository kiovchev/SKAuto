namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services.Data;
    using SKAuto.Web.ViewModels.ViewModels.UseFullCategoryViewModels;

    public class UseFullCategoryController : BaseController
    {
        private readonly IUseFullCategoryService useFullCategoryService;

        public UseFullCategoryController(IUseFullCategoryService useFullCategoryService)
        {
            this.useFullCategoryService = useFullCategoryService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult All()
        {
            List<UseFullCategoryWithImageViewModel> useFullCategoryWithImages = this.useFullCategoryService.GetAllUseFullCategoriesWithParams();

            return this.View(useFullCategoryWithImages);
        }

        public IActionResult Create()
        {
            if (this.User.IsInRole("Administrator"))
            {
                return this.View();
            }
            else
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(UseFullCategoryWithImageViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/UseFullCategory/Create");
            }
            else
            {
                bool useFullCategoryExists = this.useFullCategoryService.CheckIfExists(model.Name);
                if (useFullCategoryExists)
                {
                    return this.Redirect("/UseFullCategory/Create");
                }
                else
                {
                    await this.useFullCategoryService.CreateUseFullCategoryByNameAsync(model.Name, model.ImageAdsress);

                    return this.Redirect("/UseFullCategory/All");
                }
            }
        }
    }
}
