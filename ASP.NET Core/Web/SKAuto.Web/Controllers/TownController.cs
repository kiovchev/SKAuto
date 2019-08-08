namespace SKAuto.Web.Controllers
{
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
        public IActionResult Create(string name)
        {
            if (name == null)
            {
                return this.Redirect("/Town/Create");
            }
            else
            {
                this.townService.CreateTownByName(name);

                return this.Redirect("/Town/All");
            }
        }
    }
}
