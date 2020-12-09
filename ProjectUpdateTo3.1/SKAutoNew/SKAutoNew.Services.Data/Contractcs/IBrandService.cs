namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBrandService
    {
        Task<IList<string>> GetBrandNamesAsync();

        Task<IList<BrandWithLogoDtoModel>> GetBrandsWithLogos();

        Task<bool> IfBrandExistsAsync(string name);

        Task<int> GetBrandIdByNameAsync(string name);

        Task CreateBrand(BrandCreateDtoModel brandCreateDtoModel);

        Task<IList<BrandIndexDtoModel>> GetAllBrandsWithImageAsync();

        Task<IList<Brand>> GetAllBrandsWithModelsAsync();

        Task<bool> DeleteBrandAsync(int id);

        Task<BrandUpdateDtoModel> GetBrandByIdAsync(int id);

        Task<bool> UpdateBrandAsync(BrandUpdateDtoModel model);

        Task<Brand> GetBrandByNameAsync(string name);

        Task<int> SameBrandCount(BrandUpdateDtoModel model);
    }
}
