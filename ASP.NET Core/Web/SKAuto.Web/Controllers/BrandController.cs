namespace SKAuto.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SKAuto.Common;
    using SKAuto.Common.DtoModels.BrandDtos;
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

        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole("Administrator"))
            {
                var brands = await this.brandService.GetAllBrandsAsync();
                var brandsAll = brands.Select(x => new BrndIndexViewModel
                {
                    BrandId = x.BrandId,
                    BrandName = x.BrandName,
                }).ToList();
                return this.View(brandsAll);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> All()
        {
            var brandsWithLogos = await this.brandService.GetBrandsWithLogos();
            var brands = brandsWithLogos.Select(x => new BrandWithLogoViewModel
            {
                BrandName = x.BrandName,
                ImageAddress = x.ImageAddress,
            }).ToList();

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

            var brandDtoModel = new BrandCreateDtoModel()
            {
                Name = brandCreateInputModel.Name.ToUpper(),
                ImageAddress = brandCreateInputModel.ImageAddress,
            };
            await this.brandService.CreateBrand(brandDtoModel);

            return this.Redirect("/Brand/Index");
        }

        public async Task<IActionResult> Update(int brandId)
        {
            if (this.User.IsInRole("Administrator"))
            {
                var currentBrand = await this.brandService.GetBrandByIdAsync(brandId);
                var brand = new BrandUpdateOutputModel();
                brand.BrandId = currentBrand.BrandId;
                brand.BrandName = currentBrand.BrandName;
                brand.ImageAddress = currentBrand.ImageAddress;

                return this.View(brand);
            }

            return this.Redirect("/Identity/Account/AccessDenied");
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrandUpdateInputModel model)
        {
            var brand = new BrandUpdateDtoModel();
            brand.BrandId = model.Id;
            brand.BrandName = model.Name.ToUpper();
            brand.ImageAddress = model.ImageAddress;
            var isSame = await this.brandService.UpdateBrandAsync(brand);
            var error = new BrandError();
            error.ErrorMessage = GlobalConstants.BrandUpdateErrorMessage;

            if (isSame)
            {
                return this.RedirectToAction("Error", "Brand", error);
            }

            return this.Redirect("/Brand/Index");
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
