namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services;

    public class HomeController : BaseController
    {
        private readonly IBrandService brand;

        public HomeController(IBrandService brand)
        {
            this.brand = brand;
        }

        public async Task<IActionResult> Index()
        {
            IList<string> brandNames = await this.brand.GetBrandNamesAsync();
            brandNames = brandNames.ToList();

            return this.View(brandNames);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
