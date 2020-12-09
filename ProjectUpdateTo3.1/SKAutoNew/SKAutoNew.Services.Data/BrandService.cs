namespace SKAutoNew.Services.Data
{
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Services.Mappers.BrandServiceMappers;

    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> brands;

        public BrandService(IRepository<Brand> brands)
        {
            this.brands = brands;
        }

        public async Task CreateBrand(BrandCreateDtoModel brandCreateDtoModel)
        {
            if (brandCreateDtoModel.ImageAddress == null)
            {
                brandCreateDtoModel.ImageAddress = GlobalConstants.ImageAddress;
            }

            var brand = BrandServiceCreateMapper.Map(brandCreateDtoModel);

            await this.brands.InsertAsync(brand);
            await this.brands.SaveAsync();
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var brandWithModels = await this.brands.All()
                                            .Include(x => x.Models)
                                            .FirstOrDefaultAsync(b => b.Id == id);
            var modelCount = brandWithModels.Models.Count();

            if (modelCount > 0)
            {
                return false;
            }

            var brand = await this.brands.All().FirstOrDefaultAsync(x => x.Id == id);

            this.brands.Delete(brand);
            await this.brands.SaveAsync();

            return true;
        }

        public async Task<IList<BrandIndexDtoModel>> GetAllBrandsWithImageAsync()
        {
            var brandsAll = await this.brands.All().OrderBy(x => x.Name).ToListAsync();
            var result = BrandServiceIndexMapper.Map(brandsAll);

            return result;
        }

        public async Task<IList<Brand>> GetAllBrandsWithModelsAsync()
        {
            var brandsWithModels = await this.brands.All().Include(x => x.Models).ToListAsync();

            return brandsWithModels;
        }

        public async Task<BrandUpdateDtoModel> GetBrandByIdAsync(int id)
        {
            var currentBrand = await this.brands.All().FirstOrDefaultAsync(x => x.Id == id);
            var brandDto = BrandServiceUpdateDtoMapper.Map(currentBrand);

            return brandDto;
        }

        public async Task<Brand> GetBrandByNameAsync(string name)
        {
            var currentBrand = await this.brands.All().FirstOrDefaultAsync(x => x.Name == name);

            return currentBrand;
        }

        public async Task<int> GetBrandIdByNameAsync(string name)
        {
            var currentBrand = await this.brands.All().FirstOrDefaultAsync(x => x.Name == name);
            var brandId = currentBrand.Id;

            return brandId;
        }

        public async Task<IList<string>> GetBrandNamesAsync()
        {
            var brandNames = await this.brands.All().Select(b => b.Name).OrderBy(b => b).ToListAsync();

            return brandNames;
        }

        public async Task<IList<BrandWithLogoDtoModel>> GetBrandsWithLogos()
        {
            var allBrands = await this.brands.All().OrderBy(x => x.Name).ToListAsync();
            var brandsWithLogos = GetBrandsWithLogoMapper.Map(allBrands);

            return brandsWithLogos;
        }

        public async Task<bool> IfBrandExistsAsync(string name)
        {
            bool checkBrand = await this.brands.All().AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

            return checkBrand;
        }

        public async Task<int> SameBrandCount(BrandUpdateDtoModel model)
        {
            var sameBrandCount = await this.brands.AllAsNoTracking()
                                                  .Where(x => x.Name == model.BrandName
                                                            & x.ImageAddress == model.ImageAddress)
                                                  .CountAsync();

            return sameBrandCount;
        }

        public async Task<bool> UpdateBrandAsync(BrandUpdateDtoModel model)
        {
            var sameBrandCount = await this.SameBrandCount(model);

            if (sameBrandCount > 0)
            {
                return true;
            }

            var brand = BrandServiceUpdateInputMapper.Map(model);

            this.brands.Update(brand);
            await this.brands.SaveAsync();

            return false;
        }
    }
}
