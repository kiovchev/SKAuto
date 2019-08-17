namespace SKAuto.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Data.Models;
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
            List<Brand> allBrands = this.brandService.GetAllBrands().OrderBy(x => x.Name).ToList();
            List<BrandsWithLogosViewModel> brandsWithLogos = new List<BrandsWithLogosViewModel>();

            foreach (var brand in allBrands)
            {
                BrandsWithLogosViewModel viewModel = new BrandsWithLogosViewModel
                {
                    BrandName = brand.Name,
                    ImageAddress = brand.ImageAddress,
                };

                brandsWithLogos.Add(viewModel);
            }

            return this.View(brandsWithLogos);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateInputModel brandCreateInputModel)
        {
            if (brandCreateInputModel.Name == null)
            {
                return this.View();
            }

            var checkBrand = await this.brandService.IfBrandExistsAsync(brandCreateInputModel.Name);

            if (checkBrand)
            {
                return this.View();
            }
            else
            {
                await this.brandService.CreateBrand(brandCreateInputModel);

                return this.Redirect("/Brand/Details");
            }
        }
    }
}
