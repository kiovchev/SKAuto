namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services;

    public class HomeController : BaseController
    {
        private readonly IBrandService brand;

        public HomeController(IBrandService brand)
        {
            this.brand = brand;
        }

        public IActionResult Index()
        {
            List<string> brandNmaes = this.brand.GetAllBrands().Select(b => b.Name).ToList();

            return this.View(brandNmaes);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
