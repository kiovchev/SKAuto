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
    using SKAuto.Web.HandMappers.ModelMappers;
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
                var modelsAll = ModelIndexMapper.Map(models);

                return this.View(modelsAll);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> Kind(ModelKindInputModel kindInputModel)
        {
            var allModelsByBrand = await this.model.GetAllModelsByBrandNameAsync(kindInputModel);
            var modelsByBrand = ModelKindMapper.Map(allModelsByBrand);

            return this.View(modelsByBrand);
        }

        public async Task<IActionResult> Create()
        {
            if (this.User.IsInRole("Administrator"))
            {
                var brandNames = await this.brand.GetBrandNamesAsync();

                return this.View(brandNames);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModelInputViewModel createModel)
        {
            if (!this.ModelState.IsValid || createModel.StartYear >= createModel.EndYear)
            {
                return this.Redirect("/Model/Create");
            }

            bool existModel = await this.model
                .IfModelExistsAsync(createModel.BrandName, createModel.Name, createModel.StartYear, createModel.EndYear);

            if (existModel)
            {
                var error = new ModelError();
                error.ErrorMessage = GlobalConstants.ModelCreateErrorMessage;
                return this.RedirectToAction("Error", "Model", error);
            }

            var modelToCreate = ModelCreateMapper.Map(createModel);
            await this.model.CreateModel(modelToCreate);

            return this.RedirectToAction("Kind", "Model", new { name = createModel.BrandName });
        }

        public async Task<IActionResult> Update(int modelId)
        {
            if (this.User.IsInRole("Administrator"))
            {
                var currentModel = await this.model.GetModelByIdAsync(modelId);
                var model = ModelUpdateGetMapper.Map(currentModel);

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
                var modelToUpdate = ModelUpdatePostMapper.Map(model);
                await this.model.UpdateModelAsync(modelToUpdate);

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
