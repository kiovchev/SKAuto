namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Services;
    using SKAuto.Web.ViewModels.ViewModels;
    using SKAuto.Web.ViewModels.ViewModels.BrandViewModels;

    public class BrandController : BaseController
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        public IActionResult Details()
        {
            List<BrandsWithLogosViewModel> brandsWithLogos = this.brandService.GetBrandsWithLogos().ToList();

            return this.View(brandsWithLogos);
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
        public async Task<IActionResult> Create(BrandCreateInputModel brandCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Brand/Create");
            }

            var checkBrand = await this.brandService.IfBrandExistsAsync(brandCreateInputModel.Name);

            if (checkBrand)
            {
                return this.Redirect("/Brand/Create");
            }
            else
            {
                await this.brandService.CreateBrand(brandCreateInputModel);

                return this.Redirect("/Brand/Details");
            }
        }
    }
}
