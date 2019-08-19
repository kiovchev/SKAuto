namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
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
            IList<ModelsWithImage> modelsByBrand = await this.model.GetAllModelsAsync(kindInputModel);

            return this.View(modelsByBrand);
        }

        public async Task<IActionResult> Create()
        {
            if (this.User.IsInRole("Administrator"))
            {
                IList<string> brandNames = await this.brand.GetBrandNamesAsync();
                brandNames = brandNames.ToList();

                return this.View(brandNames);
            }
            else
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModelInputViewModel inputViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Model/Create");
            }

            if (inputViewModel.StartYear >= inputViewModel.EndYear)
            {
                return this.Redirect("/Model/Create");
            }

            bool existModel = await this.model
                .IfModelExists(inputViewModel.BrandName, inputViewModel.Name, inputViewModel.StartYear, inputViewModel.EndYear);

            if (existModel)
            {
                return this.Redirect("/Model/Create");
            }
            else
            {
                await this.model.CreateModel(inputViewModel);

                return this.RedirectToAction("Kind", "Model", new { name = inputViewModel.BrandName });
            }
        }
    }
}
