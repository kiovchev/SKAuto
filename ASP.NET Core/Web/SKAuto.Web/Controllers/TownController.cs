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

        public IActionResult ShowAll(string categoryName)
        {
            var towns = this.townService.GetTownsByCategoryName(categoryName);

            TownWithCategoryNameViewModel viewModel = new TownWithCategoryNameViewModel()
            {
                CategoryName = categoryName,
                TownNames = towns,
            };
            return this.View(viewModel);
        }

        public IActionResult All()
        {
            var neededTowns = this.townService.GetAllTowns();
            AllTownsViewModel allTowns = new AllTownsViewModel();

            foreach (var town in neededTowns)
            {
                allTowns.TownsNames.Add(town.Name);
            }

            return this.View(allTowns);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (name == null)
            {
                return this.Redirect("/Town/Create");
            }
            else
            {
                bool townExists = this.townService.CheckIfExists(name);
                if (townExists)
                {
                    return this.Redirect("/Town/Create");
                }
                else
                {
                    await this.townService.CreateTownByNameAsync(name);

                    return this.Redirect("/Town/All");
                }
            }
        }
    }
}
