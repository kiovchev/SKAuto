namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services.Data;
    using SKAuto.Web.ViewModels.ViewModels.CategoryViewModels;

    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categoriesFromDb = await this.categoryService.GetAllCategories();
            IList<CategoryIndexViewModel> categories = categoriesFromDb.Select(x => new CategoryIndexViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return this.View(categories);
        }

        public async Task<IActionResult> All()
        {
            if (this.User.IsInRole("Administrator"))
            {
                var allCategories = await this.categoryService.GetAllCategoriesForViewModel();
                IList<CategoryWithImageViewModel> categoryWithImages = allCategories.Select(x => new CategoryWithImageViewModel
                {
                    Name = x.CategoryName,
                    ImageAdsress = x.ImageAddress,
                }).ToList();

                return this.View(categoryWithImages);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public IActionResult Create()
        {
            if (this.User.IsInRole("Administrator"))
            {
                return this.View();
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryWithImageViewModel categoryModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Category/Create");
            }

            bool existCategogy = await this.categoryService.IfCategoryExists(categoryModel.Name);

            if (existCategogy)
            {
                // create a category error page
                return this.Redirect("/Category/Create");
            }

            await this.categoryService.CreateCategory(categoryModel.Name, categoryModel.ImageAdsress);

            return this.Redirect("~/Category/All");
        }

        public async Task<IActionResult> ShowAll(string modelName)
        {
            var categories = await this.categoryService.GetCategoriesByNameAndYears(modelName);

            return this.View(categories);
        }
    }
}
