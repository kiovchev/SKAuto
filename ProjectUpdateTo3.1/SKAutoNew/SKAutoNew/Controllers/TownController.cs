namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.TownViewModels;
    using System.Threading.Tasks;

    public class TownController : BaseController
    {
        private readonly ITownService townService;

        public TownController(ITownService townService)
        {
            this.townService = townService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> ShowAll(TownShallAllViewModel model)
        {
            var viewModel = await this.townService.GetTownsByCategoryNameAsync(model.CategoryName);

            // need mapper
            var currentModel = new TownWithCategoryNameViewModel
            {
                CategoryName = viewModel.CategoryName,
                TownNames = viewModel.TownNames
            };

            return this.View(currentModel);
        }

        public async Task<IActionResult> All()
        {
            var allTowns = await this.townService.GetTownNamesAsync();
            var allTownsModel = new AllTownsViewModel
            {
                TownsNames = allTowns.TownsNames
            };

            return this.View(allTownsModel);
        }

        public IActionResult Create()
        {
            if (this.User.IsInRole("Administrator"))
            {
                return this.View();
            }
            else
            {
                return this.Redirect("/Identity/Account/AccessDenied");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(TownInputViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Town/Create");
            }
            else
            {
                bool townExists = await this.townService.CheckIfExistsAsync(model.Name);
                if (townExists)
                {
                    return this.Redirect("/Town/Create");
                }
                else
                {
                    await this.townService.CreateTownByNameAsync(model.Name);

                    return this.Redirect("/Town/All");
                }
            }
        }
    }
}
