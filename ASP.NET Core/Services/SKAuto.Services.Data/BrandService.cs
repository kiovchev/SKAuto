namespace SKAuto.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Common;
    using SKAuto.Common.DtoModels.BrandDtos;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;

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

            Brand brand = new Brand
            {
                Name = brandCreateDtoModel.Name,
                ImageAddress = brandCreateDtoModel.ImageAddress,
            };

            await this.brands.AddAsync(brand);
            await this.brands.SaveChangesAsync();
        }

        public async Task<IList<BrandDeleteDtoModel>> GetAllBrandsAsync()
        {
            var brandsAll = await this.brands.All().OrderBy(x => x.Name).ToListAsync();
            var result = new List<BrandDeleteDtoModel>();

            result = brandsAll.Select(x => new BrandDeleteDtoModel
            {
                BrandId = x.Id,
                BrandName = x.Name,
            }).ToList();

            return result;
        }

        public async Task<BrandUpdateDtoModel> GetBrandByIdAsync(int id)
        {
            var currentBrand = await this.brands.All().FirstOrDefaultAsync(x => x.Id == id);
            var brandDto = new BrandUpdateDtoModel();
            brandDto.BrandId = currentBrand.Id;
            brandDto.BrandName = currentBrand.Name;
            brandDto.ImageAddress = currentBrand.ImageAddress;

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
            int brandId = currentBrand.Id;

            return brandId;
        }

        public async Task<IList<string>> GetBrandNamesAsync()
        {
            List<string> brandNames = await this.brands.All().Select(b => b.Name).OrderBy(b => b).ToListAsync();

            return brandNames;
        }

        public async Task<IList<BrandWithLogoDtoModel>> GetBrandsWithLogos()
        {
            var allBrands = await this.brands.All().OrderBy(x => x.Name).ToListAsync();

            var brandsWithLogos = allBrands.Select(x => new BrandWithLogoDtoModel
            {
                BrandName = x.Name,
                ImageAddress = x.ImageAddress,
            }).ToList();

            return brandsWithLogos;
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var brandWithModels = await this.brands.All().Include(x => x.Models).FirstOrDefaultAsync(b => b.Id == id);
            var modelCount = brandWithModels.Models.Count();

            if (modelCount > 0)
            {
                return false;
            }

            var brand = this.brands.All().FirstOrDefault(x => x.Id == id);
            this.brands.Delete(brand);
            await this.brands.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IfBrandExistsAsync(string name)
        {
            bool checkBrand = await this.brands.All().AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

            return checkBrand;
        }

        public async Task<bool> UpdateBrandAsync(BrandUpdateDtoModel model)
        {
            var sameBrand = await this.brands.AllAsNoTracking().Where(x => x.Name == model.BrandName).FirstOrDefaultAsync();

            if (sameBrand != null)
            {
                return true;
            }

            var brand = new Brand();
            brand.Id = model.BrandId;
            brand.Name = model.BrandName;

            if (model.ImageAddress == null)
            {
                model.ImageAddress = GlobalConstants.ImageAddress;
            }

            brand.ImageAddress = model.ImageAddress;

            this.brands.Update(brand);
            await this.brands.SaveChangesAsync();

            return false;
        }
    }
}
