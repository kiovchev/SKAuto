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

        public IActionResult All()
        {
            var allUseFullCategories = this.useFullCategoryService.GetAllUseFullCategories();
            List<UseFullCategoryWithImageViewModel> useFullCategoryWithImages = new List<UseFullCategoryWithImageViewModel>();

            foreach (var item in allUseFullCategories)
            {
                UseFullCategoryWithImageViewModel viewModel = new UseFullCategoryWithImageViewModel
                {
                    Name = item.Name,
                    ImageAdsress = item.ImageAddress,
                };

                useFullCategoryWithImages.Add(viewModel);
            }

            return this.View(useFullCategoryWithImages);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string imageAddress)
        {
            if (name == null)
            {
                return this.Redirect("/UseFullCategory/Create");
            }
            else
            {
                bool useFullCategoryExists = this.useFullCategoryService.CheckIfExists(name);
                if (useFullCategoryExists)
                {
                    return this.Redirect("/UseFullCategory/Create");
                }
                else
                {
                    await this.useFullCategoryService.CreateUseFullCategoryByNameAsync(name, imageAddress);

                    return this.Redirect("/UseFullCategory/All");
                }
            }
        }
    }
}
