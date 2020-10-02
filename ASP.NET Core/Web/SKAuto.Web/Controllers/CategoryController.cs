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

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult All()
        {
            List<CategoryWithImageViewModel> categoryWithImages = this.categoryService.GetAllCategoriesForViewModel().ToList();

            return this.View(categoryWithImages);
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
        public async Task<IActionResult> Create(CategoryWithImageViewModel categoryModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Category/Create");
            }

            bool existCategogy = this.categoryService.IfCategoryExists(categoryModel.Name);

            if (existCategogy)
            {
                //create a category error page
                return this.Redirect("/Category/Create");
            }
            else
            {
                await this.categoryService.CreateCategory(categoryModel.Name, categoryModel.ImageAdsress);

                return this.Redirect("~/Category/All");
            }
        }

        public IActionResult ShowAll(string modelName)
        {
            var categories = this.categoryService.GetCategoriesByNameAndYears(modelName).ToList();

            return this.View(categories);
        }
    }
}
