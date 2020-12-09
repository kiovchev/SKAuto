namespace SKAutoNew.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SKAutoNew.Common;
    using SKAutoNew.HandMappers.BrandMappers;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Web.ViewModels.BrandViewModels;
    using System.Threading.Tasks;

    public class BrandController : BaseController
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole("Administrator"))
            {
                var brands = await this.brandService.GetAllBrandsWithImageAsync();
                var brandsAll = BrandIndexMapper.Map(brands);

                return this.View(brandsAll);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> All()
        {
            var brandsWithLogos = await this.brandService.GetBrandsWithLogos();
            var brands = BrandAllMapper.Map(brandsWithLogos);

            return this.View(brands);
        }

        public IActionResult Create()
        {
            if (this.User.IsInRole("Administrator"))
            {
                return this.View();
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateInputModel brandCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Brand/Create");
            }

            var checkBrand = await this.brandService.IfBrandExistsAsync(brandCreateInputModel.Name.ToUpper());

            if (checkBrand)
            {
                var error = new BrandError();
                error.ErrorMessage = GlobalConstants.BrandCreateErrorMessage;
                return this.RedirectToAction("Error", "Brand", error);
            }

            var brandDtoModel = BrandCreateMapper.Map(brandCreateInputModel);
            await this.brandService.CreateBrand(brandDtoModel);

            return this.Redirect("/Brand/Index");
        }

        public async Task<IActionResult> Update(int brandId)
        {
            if (this.User.IsInRole("Administrator"))
            {
                var currentBrand = await this.brandService.GetBrandByIdAsync(brandId);
                var brand = BrandUpdateGetMapper.Map(currentBrand);

                return this.View(brand);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrandUpdateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("/Brand/Index");
            }

            if (this.User.IsInRole("Administrator"))
            {
                var brand = BrandUpdatePostMapper.Map(model);
                var isSame = await this.brandService.UpdateBrandAsync(brand);
                var error = new BrandError();
                error.ErrorMessage = GlobalConstants.BrandUpdateErrorMessage;

                if (isSame)
                {
                    return this.RedirectToAction("Error", "Brand", error);
                }

                return this.Redirect("/Brand/Index");
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> Delete(int brandId)
        {
            if (this.User.IsInRole("Administrator"))
            {
                var haveModels = await this.brandService.DeleteBrandAsync(brandId);

                if (!haveModels)
                {
                    var error = new BrandError();
                    error.ErrorMessage = GlobalConstants.BrandDeleteErrorMessage;
                    return this.RedirectToAction("Error", "Brand", error);
                }

                return this.Redirect("/Brand/Index");
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public IActionResult Error(BrandError brandError)
        {
            return this.View(brandError);
        }
    }
}
