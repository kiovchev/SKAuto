namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Common;
    using SKAuto.Common.DtoModels.ModelDto;
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

        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole("Administrator"))
            {
                var models = await this.model.GetAllModels();
                var modelsAll = models.Select(x => new ModelWithBrandNameOutputModel
                {
                    ModelId = x.ModelId,
                    ModelName = x.ModelName,
                    StartYear = x.StartYear,
                    EndYear = x.EndYear,
                    ImageAddress = x.ImageAddress,
                    BrandName = x.BrandName,
                })
                    .OrderBy(x => x.BrandName)
                    .ThenBy(x => x.ModelName)
                    .ToList();
                return this.View(modelsAll);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> Kind(ModelKindInputModel kindInputModel)
        {
            IList<ModelsWithImage> modelsByBrand = await this.model.GetAllModelsByBrandNameAsync(kindInputModel);

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

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModelInputViewModel inputViewModel)
        {
            if (!this.ModelState.IsValid || inputViewModel.StartYear >= inputViewModel.EndYear)
            {
                return this.Redirect("/Model/Create");
            }

            bool existModel = await this.model
                .IfModelExistsAsync(inputViewModel.BrandName, inputViewModel.Name, inputViewModel.StartYear, inputViewModel.EndYear);

            if (existModel)
            {
                var error = new ModelError();
                error.ErrorMessage = GlobalConstants.ModelCreateErrorMessage;
                return this.RedirectToAction("Error", "Model", error);
            }

            await this.model.CreateModel(inputViewModel);

            return this.RedirectToAction("Kind", "Model", new { name = inputViewModel.BrandName });
        }

        public async Task<IActionResult> Update(int modelId)
        {
            if (this.User.IsInRole("Administrator"))
            {
                var currentModel = await this.model.GetModelByIdAsync(modelId);
                var model = new ModelUpdateOutputModel()
                {
                    ModelId = currentModel.ModelId,
                    ModelName = currentModel.ModelName,
                    StartYear = currentModel.StartYear,
                    EndYear = currentModel.EndYear,
                    ImageAddress = currentModel.ImageAddress,
                    BrandName = currentModel.BrandName,
                    AllBrandNames = currentModel.AllBrandNames,
                };

                return this.View(model);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ModelUpdateInputModel model)
        {
            var isSameModel = await this.model
                .IsSameAsync(model.BrandName, model.Name, model.StartYear, model.EndYear, model.ImageAddress);

            if (!isSameModel)
            {
                var updatedModel = new ModelUpdateInputDtoModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    StartYear = model.StartYear,
                    EndYear = model.EndYear,
                    ImageAddress = model.ImageAddress,
                    BrandName = model.BrandName,
                };

                await this.model.UpdateModelAsync(updatedModel);

                return this.Redirect("/Model/Index");
            }

            var error = new ModelError();
            error.ErrorMessage = GlobalConstants.ModelUpdateErrorMessage;

            return this.RedirectToAction("Error", "Model", error);
        }

        public async Task<IActionResult> Delete(int modelId)
        {
            if (this.User.IsInRole("Administrator"))
            {
                var haveParts = await this.model.HavePartsAsync(modelId);

                if (haveParts)
                {
                    var error = new ModelError();
                    error.ErrorMessage = GlobalConstants.ModelDeleteErrorMessage;

                    return this.RedirectToAction("Error", "Model", error);
                }

                await this.model.DeleteModelAsync(modelId);

                return this.Redirect("/Model/Index");
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public IActionResult Error(ModelError modelError)
        {
            return this.View(modelError);
        }
    }
}
