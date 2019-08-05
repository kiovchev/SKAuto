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
            List<Brand> allBrands = this.brandService.GetAllBrands().ToList();
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
        public async Task<IActionResult> Create(string name, string imageAddress)
        {
            if (name == null)
            {
                return this.View();
            }

            // var brandName = string.Empty;
            // var currentName = name.Split(" ", System.StringSplitOptions.RemoveEmptyEntries).ToArray();
            // if (currentName.Length > 1)
            // {
            //    currentName = currentName.Select(x => x.ToUpper()).ToArray();
            //    brandName = string.Join("-", currentName);
            // }
            var currentBrand = this.brandService.GetAllBrands().FirstOrDefault(x => x.Name == name);

            if (currentBrand != null)
            {
                return this.View();
            }
            else
            {
                BrandCreateInputModel brandCreateInputModel = new BrandCreateInputModel
                {
                    Name = name.ToUpper(),
                    ImageAddress = imageAddress,
                };

                await this.brandService.CreateBrand(brandCreateInputModel);

                return this.Redirect("/Brand/Details");
            }
        }
    }
}
