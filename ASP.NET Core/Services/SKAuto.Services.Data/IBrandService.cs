namespace SKAuto.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Data.Models;

    public interface IBrandService
    {
        Task<IList<string>> GetBrandNamesAsync();

        Task<IList<BrandWithLogoDtoModel>> GetBrandsWithLogos();

        Task<bool> IfBrandExistsAsync(string name);

        Task<int> GetBrandIdByNameAsync(string name);

        Task CreateBrand(BrandCreateDtoModel brandCreateDtoModel);

        Task<IList<BrandDeleteDtoModel>> GetAllBrandsAsync();

        Task<IList<BrandIndexDtoModel>> GetAllBrandsWithImageAsync();

        Task<bool> DeleteBrandAsync(int id);

        Task<BrandUpdateDtoModel> GetBrandByIdAsync(int id);

        Task<bool> UpdateBrandAsync(BrandUpdateDtoModel model);

        Task<Brand> GetBrandByNameAsync(string name);
    }
}
