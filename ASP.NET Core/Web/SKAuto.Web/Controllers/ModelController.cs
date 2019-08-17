namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Data.Models;
    using SKAuto.Services;
    using SKAuto.Services.Data;
    using SKAuto.Web.ViewModels.ViewModels.ModelViewModels;

    public class ModelController : BaseController
    {
        private readonly IBrandService brand;
        private readonly IModelService model;

        public ModelController(IBrandService brand, IModelService model)
        {
            this.brand = brand;
            this.model = model;
        }

        public async Task<IActionResult> Kind(ModelKindInputModel kindInputModel)
        {
            var brandId = await this.brand.GetBrandIdByNameAsync(kindInputModel.Name);
            List<Model> models = this.model.GetAllModelsByBrandId(brandId).ToList();

            List<ModelsWithImage> modelsByBrand = new List<ModelsWithImage>();

            foreach (var item in models)
            {
                ModelsWithImage modelsWithImage = new ModelsWithImage
                {
                    Name = kindInputModel.Name + " " + item.Name + " " + item.StartYear + "-" + item.EndYear,
                    ModelImageAddress = item.ImageAddress,
                };

                modelsByBrand.Add(modelsWithImage);
            }

            return this.View(modelsByBrand);
        }

        public async Task<IActionResult> Create()
        {
            IList<string> brandNames = await this.brand.GetBrandNamesAsync();
            brandNames = brandNames.ToList();

            return this.View(brandNames);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string brandName, string modelName, string startYear, string endYear, string imageAddress)
        {
            int brandId = await this.brand.GetBrandIdByNameAsync(brandName);
            var allModels = this.model.GetAllModelsByBrandId(brandId);
            bool existModel = allModels.Any(x => x.Name == modelName);

            if (existModel)
            {
                return this.View();
            }
            else
            {
                ModelInputViewModel modelInputViewModel = new ModelInputViewModel
                {
                    Name = modelName,
                    StartYear = int.Parse(startYear),
                    EndYear = int.Parse(endYear),
                    BrandName = brandName,
                    ImageAddress = imageAddress,
                };

                await this.model.CreateModel(modelInputViewModel, brandId);

                return this.RedirectToAction("Kind", "Model", new { name = brandName });
            }
        }
    }
}
