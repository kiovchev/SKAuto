namespace SKAuto.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.BrandViewModels;

    public interface IBrandService
    {
        Task<IList<string>> GetBrandNamesAsync();

        Task<bool> IfBrandExistsAsync(string name);

        IQueryable<Brand> GetAllBrands();

        Task<int> GetBrandIdByNameAsync(string name);

        Task CreateBrand(BrandCreateInputModel brandCreateInputModel);
    }
}
