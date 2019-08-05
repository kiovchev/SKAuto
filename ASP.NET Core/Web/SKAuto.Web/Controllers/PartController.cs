namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Data.Models;
    using SKAuto.Services;
    using SKAuto.Services.Data;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public class PartController : BaseController
    {
        private readonly IPartService partService;
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;

        public PartController(IPartService partService, IBrandService brandService, ICategoryService categoryService)
        {
            this.partService = partService;
            this.brandService = brandService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> All(string modelName, string categoryName)
        {
            List<PartByCategoryAndModelViewModel> neededParts = await this.partService.GetPartsByModelAndCategoryAsync(modelName, categoryName);
            neededParts = neededParts.OrderBy(x => x.PartName).ToList();

            return this.View(neededParts);
        }

        public IActionResult Create()
        {
            List<Brand> brands = this.brandService.GetAllBrands().ToList();
            List<Category> categories = this.categoryService.GetAllCategories().ToList();

            List<string> brandsWithModels = new List<string>();

            foreach (var brand in brands)
            {
                foreach (var model in brand.Models)
                {
                    string neededInfo = brand.Name + " " + model.Name + " " + model.StartYear + "-" + model.EndYear;

                    brandsWithModels.Add(neededInfo);
                }
            }

            PartCreateViewModel partCreate = new PartCreateViewModel
            {
                BrandWithModels = brandsWithModels,
                Categories = categories,
            };

            return this.View(partCreate);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartCreateInputModel model)
        {
            await this.partService.CreatePartAsync(model);

            return this.RedirectToAction("All", "Part", new { modelName = model.ModelName, categoryName = model.CategoryName });
        }
    }
}
