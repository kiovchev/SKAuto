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
    using SKAutoNew.Services.Data.EfExtensionsHelper;

    /// <summary>
    /// business logic for Brand
    /// </summary>
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> brands;

        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="brands"></param>
        public BrandService(IRepository<Brand> brands)
        {
            this.brands = brands;
        }

        /// <summary>
        /// method - checks image addres param, create new brand and add it to the database
        /// </summary>
        /// <param name="brandCreateDtoModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// method - delete current brand if there are no any models for it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

            this.brands.Delete(brandWithModels);
            await this.brands.SaveAsync();

            return true;
        }

        /// <summary>
        /// method - get all brands from database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<BrandIndexDtoModel>> GetAllBrandsWithImageAsync()
        {
            var brandsAll = this.brands.All();
            var allBrands = await EfExtensions.ToListAsyncSafe<Brand>(brandsAll);
            var result = BrandServiceIndexMapper.Map(allBrands);

            return result;
        }

        /// <summary>
        /// method - get all brands include models for every brand (form database)
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Brand>> GetAllBrandsWithModelsAsync()
        {
            var brandsWithModels = this.brands.All().Include(x => x.Models);
            var allBrandsWithModels = await EfExtensions.ToListAsyncSafe<Brand>(brandsWithModels);

            return allBrandsWithModels;
        }

        /// <summary>
        /// method - find and get brand by id param from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BrandUpdateDtoModel> GetBrandByIdAsync(int id)
        {
            var brandsAll = this.brands.All();
            var allBrands = await EfExtensions.ToListAsyncSafe<Brand>(brandsAll);
            var currentBrand = allBrands.FirstOrDefault(x => x.Id == id);
            var brandDto = BrandServiceUpdateDtoMapper.Map(currentBrand);

            return brandDto;
        }

        /// <summary>
        /// method - find and get brand by name param from database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Brand> GetBrandByNameAsync(string name)
        {
            var brandsWithModels = this.brands.All();
            var allBrandsWithModels = await EfExtensions.ToListAsyncSafe<Brand>(brandsWithModels);
            var currentBrand = allBrandsWithModels.FirstOrDefault(x => x.Name == name);
            //var currentBrand = await this.brands.All().FirstOrDefaultAsync(x => x.Name == name);

            return currentBrand;
        }

        /// <summary>
        /// method - find and get brand by name param from database, return brand id param
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<int> GetBrandIdByNameAsync(string name)
        {
            var brandsAll = this.brands.All();
            var allBrands = await EfExtensions.ToListAsyncSafe<Brand>(brandsAll);
            var currentBrand = allBrands.FirstOrDefault(x => x.Name == name);
            //var currentBrand = await this.brands.All().FirstOrDefaultAsync(x => x.Name == name);
            var brandId = currentBrand.Id;

            return brandId;
        }

        /// <summary>
        /// method - get all brans from database and return all brands names
        /// </summary>
        /// <returns></returns>
        public async Task<IList<string>> GetBrandNamesAsync()
        {
            var brandsAll = this.brands.All();
            var allBrands = await EfExtensions.ToListAsyncSafe<Brand>(brandsAll);
            var brandNames = allBrands.Select(b => b.Name).OrderBy(b => b).ToList();
            //var brandNames = await this.brands.All().Select(b => b.Name).OrderBy(b => b).ToListAsync();

            return brandNames;
        }

        /// <summary>
        /// method - get brands with their images from database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<BrandWithLogoDtoModel>> GetBrandsWithLogos()
        {
            var brandsAll = this.brands.All();
            var allBrands = await EfExtensions.ToListAsyncSafe<Brand>(brandsAll);
            var allOrderedBrands = allBrands.OrderBy(x => x.Name).ToList();
            var brandsWithLogos = GetBrandsWithLogoMapper.Map(allOrderedBrands);

            return brandsWithLogos;
        }

        /// <summary>
        /// method - check in databse, is there a brand with a name which we take like a param
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> IfBrandExistsAsync(string name)
        {
            var brandsAll = this.brands.All();
            var allBrands = await EfExtensions.ToListAsyncSafe<Brand>(brandsAll);
            bool checkBrand = allBrands.Any(x => x.Name.ToUpper() == name.ToUpper());

            return checkBrand;
        }

        /// <summary>
        /// method - check in databese is there same brand, like param model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> SameBrandCount(BrandUpdateDtoModel model)
        {
            var brandsAll = this.brands.AllAsNoTracking();
            var allBrands = await EfExtensions.ToListAsyncSafe<Brand>(brandsAll);
            var sameBrandCount = allBrands.Where(x => x.Name == model.BrandName
                                                    & x.ImageAddress == model.ImageAddress)
                                                    .Count();

            return sameBrandCount;
        }

        /// <summary>
        /// method - update brand in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
