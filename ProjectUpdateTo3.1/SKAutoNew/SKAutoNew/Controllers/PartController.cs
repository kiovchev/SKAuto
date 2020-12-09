namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.HandMappers.PartMappers;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.PartViewModels;
    using System.Threading.Tasks;

    public class PartController : BaseController
    {
        private readonly IPartService partService;

        public PartController(IPartService partService)
        {
            this.partService = partService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole("Administrator"))
            {
                var dtoAllPartsModel = await this.partService.GetAllPartsAsync();
                var partsAll = PartIndexMaper.Map(dtoAllPartsModel);
                return this.View(partsAll);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> All(PartParamsInputModel model)
        {
            // change prop name from ModelName to BrandAndModelName
            var neededDtoParts = await this.partService.GetPartsByModelAndCategoryAsync(model.ModelName, model.CategoryName);
            var neededParts = PartAllMapper.Map(neededDtoParts);

            return this.View(neededParts);
        }

        public async Task<IActionResult> Create()
        {
            if (this.User.IsInRole("Administrator"))
            {
                var partDtoCreate = await this.partService.GetPartCreateParams();
                var partCreate = PartCreateOutPutMapper.Map(partDtoCreate);

                return this.View(partCreate);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartCreateInputModel model)
        {
            if (this.User.IsInRole("Administrator"))
            {
                if (!this.ModelState.IsValid)
                {
                    return this.Redirect("/Part/Create");
                }

                var ifPartExists = await this.partService.CheckIfPartExistsAsync(
                                                                                 model.PartName,
                                                                                 model.BrandAndModelName,
                                                                                 model.CategoryName,
                                                                                 model.ManufactoryName);

                if (ifPartExists)
                {
                    return this.Redirect("/Part/Error");
                }

                var dtoPartModel = PartCreateInputMapper.Map(model);
                await this.partService.CreatePartAsync(dtoPartModel);

                return this.RedirectToAction("All", "Part", new { modelName = model.BrandAndModelName, categoryName = model.CategoryName });
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> Add(PartAddGetModel model)
        {
            if (this.User.IsInRole("Administrator"))
            {
                var partDto = await this.partService.GetAddOutputModelByIdAsync(model.PartId);
                var partAddOutputModel = PartAddOutputMapper.Map(partDto);

                return this.View(partAddOutputModel);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Add(PartAddInputModel partAdd)
        {
            if (this.User.IsInRole("Administrator"))
            {
                if (!this.ModelState.IsValid)
                {
                    return this.Redirect("/Part/Index");
                }

                var partDto = PartAddInputMapper.Map(partAdd);
                await this.partService.AddQuantityAsync(partDto);

                return this.Redirect("/Part/Index");
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> Update(PartUpdateGetModel model)
        {
            var partDto = await this.partService.GetPartUpdateModel(model.PartId);
            var partModel = PartUpdateOutputMapper.Map(partDto);

            return this.View(partModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PartUpdateInputModel inputModel)
        {
            if (this.User.IsInRole("Administrator"))
            {
                if (!this.ModelState.IsValid)
                {
                    return this.Redirect("/Part/Index");
                }

                var isSamePart = await this.partService.IsSamePartAsync(
                                                                        inputModel.PartName,
                                                                        inputModel.BrandAndModelName,
                                                                        inputModel.CategoryName,
                                                                        inputModel.ManufactoryName,
                                                                        inputModel.Quantity,
                                                                        inputModel.Price);

                if (isSamePart)
                {
                    return this.Redirect("/Part/Error");
                }

                var partDto = PartUpdateInputMapper.Map(inputModel);
                await this.partService.UpdatePartAsync(partDto);

                return this.Redirect("/Part/Index");
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> Delete(int partId)
        {
            if (this.User.IsInRole("Administrator"))
            {
                await this.partService.DeletePartAsync(partId);

                return this.Redirect("/Part/Index");
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
