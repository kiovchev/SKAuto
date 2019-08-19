namespace SKAuto.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Web.ViewModels.ViewModels;
    using SKAuto.Web.ViewModels.ViewModels.BrandViewModels;

    public interface IBrandService
    {
        Task<IList<string>> GetBrandNamesAsync();

        IList<BrandsWithLogosViewModel> GetBrandsWithLogos();

        Task<bool> IfBrandExistsAsync(string name);

        Task<int> GetBrandIdByNameAsync(string name);

        Task CreateBrand(BrandCreateInputModel brandCreateInputModel);
    }
}
