namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.Common;
    using SKAutoNew.HandMappers.ModelMappers;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.ModelViewModels;
    using System.Threading.Tasks;

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
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                var models = await this.model.GetAllModels();
                var modelsAll = ModelIndexMapper.Map(models);

                return this.View(modelsAll);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> Kind(ModelKindInputModel kindInputModel)
        {
            var allModelsByBrand = await this.model.GetAllModelsByBrandNameAsync(kindInputModel.Name);
            var modelsByBrand = ModelKindMapper.Map(allModelsByBrand);

            return this.View(modelsByBrand);
        }

        public async Task<IActionResult> Create()
        {
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                var brandNames = await this.brand.GetBrandNamesAsync();

                return this.View(brandNames);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModelInputViewModel createModel)
        {
            if (!this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }

            if (!this.ModelState.IsValid || createModel.StartYear >= createModel.EndYear)
            {
                return this.Redirect("/Model/Create");
            }

            bool existModel = await this.model.IfModelExistsAsync(createModel.BrandName,
                                                                  createModel.Name, 
                                                                  createModel.StartYear, 
                                                                  createModel.EndYear);

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
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
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
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                if (!this.ModelState.IsValid)
                {
                    return this.Redirect("/Model/Index");
                }

                var isSameModel = await this.model.IsSameAsync(model.BrandName,
                                                               model.Name, 
                                                               model.StartYear, 
                                                               model.EndYear,
                                                               model.ImageAddress);

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

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> Delete(int modelId)
        {
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName))
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
