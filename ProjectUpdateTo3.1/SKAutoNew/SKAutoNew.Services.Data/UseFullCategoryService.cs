namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Mappers.UseFullCategoryServiceMappers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// business logic for UseFullCategory
    /// </summary>
    public class UseFullCategoryService : IUseFullCategoryService
    {
        private readonly IRepository<UseFullCategory> useFullCategories;
        private readonly IRepository<Town> towns;
        private readonly IRepository<TownUseFullCategory> townUseFullCategories;
        private readonly IRepository<Company> companyRepo;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="useFullCategories"></param>
        /// <param name="towns"></param>
        /// <param name="townUseFullCategories"></param>
        /// <param name="companyRepo"></param>
        public UseFullCategoryService(
            IRepository<UseFullCategory> useFullCategories,
            IRepository<Town> towns,
            IRepository<TownUseFullCategory> townUseFullCategories,
            IRepository<Company> companyRepo)
        {
            this.useFullCategories = useFullCategories;
            this.towns = towns;
            this.townUseFullCategories = townUseFullCategories;
            this.companyRepo = companyRepo;
        }

        /// <summary>
        /// check is there same category in database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfExistsAsync(string name)
        {
            bool checkUseFullCategory = await this.useFullCategories
                                                              .All()
                                                              .AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

            return checkUseFullCategory;
        }

        /// <summary>
        /// create new usefullcategory, townusefullcategories and add them all in database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="imageAddress"></param>
        /// <returns></returns>
        public async Task CreateUseFullCategoryByNameAsync(string name, string imageAddress)
        {
            var towns = this.towns.All();

            if (imageAddress == null)
            {
                imageAddress = GlobalConstants.ImageAddress;
            }

            var useFullCategory = new UseFullCategory()
            {
                Name = name,
                ImageAddress = imageAddress,
            };

            foreach (var town in towns)
            {
                var townUseFullCategory = new TownUseFullCategory()
                {
                    UseFullCategory = useFullCategory,
                    Town = town,
                };

                await this.townUseFullCategories.InsertAsync(townUseFullCategory);
            }

            await this.useFullCategories.InsertAsync(useFullCategory);
            await this.useFullCategories.SaveAsync();
        }

        /// <summary>
        /// delete category and townusefullcategories that are conected with it from database
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUseFullCategoryAsync(UseFullDeleteDtoModel dtoModel)
        {
            var modelToDelete = await this.useFullCategories.All()
                                                      .Include(x => x.Towns)
                                                      .Include(x => x.Companies)
                                                      .FirstOrDefaultAsync(x => x.Id == dtoModel.UseFullCategoryId);

            if (modelToDelete == null)
            {
                return true;
            }

            foreach (var item in modelToDelete.Companies.ToList())
            {
                this.companyRepo.Delete(item);
            }

            foreach (var item in modelToDelete.Towns.ToList())
            {
                this.townUseFullCategories.Delete(item);
            }

            this.useFullCategories.Delete(modelToDelete);
            await this.towns.SaveAsync();

            return false;
        }

        /// <summary>
        /// get all usefullcategories from database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<IndexDtoModel>> GetAllCategoriesForIndexAsync()
        {
            var allCategories = await this.useFullCategories.All().ToListAsync();
            var dtomodels = UseFullIndexServiceMapper.Map(allCategories);

            return dtomodels;
        }

        /// <summary>
        /// get all usefullcategories from database for company service
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UseFullCategory>> GetAlluseFullCategoriesAsync()
        {
            var allCategories = await this.useFullCategories.All().ToListAsync();

            return allCategories;
        }

        /// <summary>
        /// get all usefullcategories with images rom database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UseFullCategoryWithImageViewDtoModel>> GetAllUseFullCategoriesWithParamsAsync()
        {
            var allUseFullCategories = await this.useFullCategories.All().ToListAsync();
            var useFullCategoryWithImages = allUseFullCategories
                                            .Select(x => new UseFullCategoryWithImageViewDtoModel
                                                             {
                                                                 Name = x.Name,
                                                                 ImageAddress = x.ImageAddress
                                                             }).ToList();

            return useFullCategoryWithImages;
        }

        /// <summary>
        /// get dto model needed for update view 
        /// </summary>
        /// <param name="inputDtoModel"></param>
        /// <returns></returns>
        public async Task<UseFullUpdateGetOutputDtoModel> GetDtoModelForUpdateOutputModelAsync(UseFullUpdateGetInputDtoModel inputDtoModel)
        {
            var neededCategory = await this.useFullCategories.All()
                                                             .FirstOrDefaultAsync(x => x.Id == inputDtoModel.UseFullCategoryId);

            if (neededCategory == null)
            {
                return null;
            }

            var dtoOutputModel = UseFullUpdateGetOutPutServiceMapper.Map(neededCategory);

            return dtoOutputModel;
        }

        /// <summary>
        /// change usefullcategory and add changes to database
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUseFullCategoryAsync(UseFullUpdatePostInputDtoModel dtoModel)
        {
            var allmodels = await this.useFullCategories.All().ToListAsync();

            if (allmodels.Any(x => x.ImageAddress == dtoModel.ImageAddress 
                                && x.Name == dtoModel.UseFullCategoryName))
            {
                return true;
            }

            var modelToUpdate = allmodels.FirstOrDefault(x => x.Id == dtoModel.UseFullCategoryId);
            modelToUpdate = UseFullUpdatePostIputServiceMapper.Map(modelToUpdate, dtoModel);

            this.useFullCategories.Update(modelToUpdate);
            await this.useFullCategories.SaveAsync();

            return false;
        }
    }
}
