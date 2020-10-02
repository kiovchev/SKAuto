namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services.Data;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public class PartController : BaseController
    {
        private readonly IPartService partService;

        public PartController(IPartService partService)
        {
            this.partService = partService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All(PartParamsInputModel model)
        {
            List<PartByCategoryAndModelViewModel> neededParts = await this.partService.GetPartsByModelAndCategoryAsync(model.ModelName, model.CategoryName);
            neededParts = neededParts.OrderBy(x => x.PartName).ToList();

            return this.View(neededParts);
        }

        public IActionResult Create()
        {
            if (this.User.IsInRole("Administrator"))
            {
                PartCreateViewModel partCreate = this.partService.GetPartCreateParams();

                return this.View(partCreate);
            }
            else
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Part/Create");
            }

            var ifPartExists = await this.partService.CheckIfPartExistsAsync(model);

            if (!ifPartExists)
            {
                // to create a part error page
                return this.Redirect("/Part/Error");
            }

            await this.partService.CreatePartAsync(model);

            return this.RedirectToAction("All", "Part", new { modelName = model.ModelName, categoryName = model.CategoryName });
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
