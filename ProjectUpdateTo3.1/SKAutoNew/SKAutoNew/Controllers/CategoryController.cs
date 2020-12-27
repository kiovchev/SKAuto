namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.Common;
    using SKAutoNew.HandMappers.CategoryMappers;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.CategoryViewModels;
    using System.Threading.Tasks;

    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var categoriesFromDb = await this.categoryService.GetAllCategoriesAsync();
            var categories = CategoryIndexMapper.Map(categoriesFromDb);

            return this.View(categories);
        }

        public async Task<IActionResult> All()
        {
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                var allCategories = await this.categoryService.GetAllCategoriesForViewModelAsync();
                var categoryWithImages = CategoryAllMapper.Map(allCategories);

                return this.View(categoryWithImages);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public IActionResult Create()
        {
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.View();
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryWithImageViewModel categoryModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Category/Create");
            }

            bool existCategogy = await this.categoryService.IfCategoryExists(categoryModel.Name);

            if (existCategogy)
            {
                var error = new CategoryError();
                error.ErrorMessage = GlobalConstants.CategotyCreateErrorMessage;
                return this.RedirectToAction("Error", "Model", error);
            }

            var categoryToCreate = CategoryCreateMapper.Map(categoryModel);
            await this.categoryService.CreateCategoryAsync(categoryToCreate);

            return this.Redirect("~/Category/All");
        }

        public async Task<IActionResult> ShowAll(string modelName)
        {
            var categories = await this.categoryService.GetCategoriesByNameAndYears(modelName);
            var neededCategories = CategoryShallAllMapper.Map(categories);

            return this.View(neededCategories);
        }

        public async Task<IActionResult> Update(CategoryUpdateIdmodel categoryModel)
        {
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                if (ModelState.IsValid)
                {
                    var category = await this.categoryService.GetCategoryByIdAsync(categoryModel.CategoryId);
                    var neededCategory = CategoryUpdateOutputMapper.Map(category);

                    return this.View(neededCategory);
                }
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateInputModel category)
        {
            if (!ModelState.IsValid)
            {
                var error = new CategoryError();
                error.ErrorMessage = GlobalConstants.CategotyinputModelUpdateErrorMessage;
                return this.RedirectToAction("Error", "Model", error);
            }

            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                var isSame = await this.categoryService
                    .IsSameCategoryAsync(category.Name, category.ImageAddress);

                if (isSame)
                {
                    var error = new CategoryError();
                    error.ErrorMessage = GlobalConstants.CategotyUpdateErrorMessage;
                    return this.RedirectToAction("Error", "Model", error);
                }


                var categoryDto = CategoryUpdateInputMapper.Map(category);
                await this.categoryService.UpdateCategoryAsync(categoryDto);

                return this.Redirect("/Category/Index");
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> Delete(int categoryId)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            var isDeleted = await this.categoryService.DeleteCategoryAsync(categoryId);

            if (isDeleted)
            {
                return this.Redirect("/Category/Index");
            }

            var error = new CategoryError();
            error.ErrorMessage = GlobalConstants.CategoryDeleteErrorMessage;
            return this.RedirectToAction("Error", "Model", error);
        }

        public IActionResult Error(CategoryError modelError)
        {
            return this.View(modelError);
        }
    }
}
