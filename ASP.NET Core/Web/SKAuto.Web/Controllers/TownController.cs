namespace SKAuto.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services.Data;
    using SKAuto.Web.ViewModels.ViewModels.TownViewModels;

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

        public IActionResult ShowAll(TownShallAllViewModel model)
        {
            // change input model and delete that one
            var viewModel = this.townService.GetTownsByCategoryName(model.CategoryName);

            return this.View(viewModel);
        }

        public IActionResult All()
        {
            AllTownsViewModel allTowns = this.townService.GetTownNames();

            return this.View(allTowns);
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
                bool townExists = this.townService.CheckIfExists(model.Name);
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
