namespace SKAuto.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.BrandViewModels;

    public interface IBrandService
    {
        IQueryable<Brand> GetAllBrands();

        Task<int> GetBrandIdByNameAsync(string name);

        Task CreateBrand(BrandCreateInputModel brandCreateInputModel);
    }
}
