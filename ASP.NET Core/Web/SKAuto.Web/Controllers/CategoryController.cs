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

        public IActionResult All()
        {
            var allCategories = this.categoryService.GetAllCategories();
            List<CategoryWithImageViewModel> categoryWithImages = new List<CategoryWithImageViewModel>();

            foreach (var item in allCategories)
            {
                CategoryWithImageViewModel viewModel = new CategoryWithImageViewModel
                {
                    Name = item.Name,
                    ImageAdsress = item.ImageAddress,
                };

                categoryWithImages.Add(viewModel);
            }

            return this.View(categoryWithImages);
        }

        public IActionResult Types(string name)
        {
            var allCategories = this.categoryService.GetAllCategories().Where(x => x.ModelCategories.Any(y => y.Model.Name == name)).ToList();

            return null;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string imageAddress)
        {
            var allCategories = this.categoryService.GetAllCategories();
            bool existCategogy = allCategories.Any(x => x.Name == name);

            if (existCategogy)
            {
                return this.View();
            }
            else
            {
                await this.categoryService.CreateCategory(name, imageAddress);

                return this.Redirect("~/Category/All");
            }
        }

        public IActionResult ShowAll(string modelName)
        {
            var categories = this.categoryService.GetCategoriesByNameAndYears(modelName);

            List<CategoryWithModelViewModel> neededCategory = new List<CategoryWithModelViewModel>();

            foreach (var item in categories)
            {
                string catName = item.Name;
                string catImage = item.ImageAddress;

                CategoryWithModelViewModel category = new CategoryWithModelViewModel
                {
                    Name = catName,
                    ImageAdsress = catImage,
                    ModelName = modelName,
                };

                neededCategory.Add(category);
            }

            return this.View(neededCategory);
        }
    }
}
