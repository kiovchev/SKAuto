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

    public class UseFullCategoryService : IUseFullCategoryService
    {
        private readonly IRepository<UseFullCategory> useFullCategories;
        private readonly IRepository<Town> towns;
        private readonly IRepository<TownUseFullCategory> townUseFullCategories;
        private readonly IRepository<Company> companyRepo;

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

        public async Task<bool> CheckIfExistsAsync(string name)
        {
            bool checkUseFullCategory = await this.useFullCategories
                                                              .All()
                                                              .AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

            return checkUseFullCategory;
        }

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

        public async Task<IList<IndexDtoModel>> GetAllCategoriesForIndexAsync()
        {
            var allCategories = await this.useFullCategories.All().ToListAsync();
            var dtomodels = UseFullIndexServiceMapper.Map(allCategories);

            return dtomodels;
        }

        public async Task<IList<UseFullCategory>> GetAlluseFullCategoriesAsync()
        {
            var allCategories = await this.useFullCategories.All().ToListAsync();

            return allCategories;
        }

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

        public async Task<UseFullUpdateGetOutputDtoModel> GetDtoModelForUpdateOutputModelAsync(UseFullUpdateGetInputDtoModel inputDtoModel)
        {
            var neededCategory = await this.useFullCategories.All()
                                                             .FirstOrDefaultAsync(x => x.Id == inputDtoModel.UseFullCategoryId);

            var dtoOutputModel = UseFullUpdateGetOutPutServiceMapper.Map(neededCategory);

            return dtoOutputModel;
        }

        public async Task<bool> UpdateUseFullCategoryAsync(UseFullUpdatePostInputDtoModel dtoModel)
        {
            var allmodels = await this.useFullCategories.All().ToListAsync();

            if (allmodels.Any(x => x.ImageAddress == dtoModel.ImageAddress 
                                && x.Name == dtoModel.UseFullCategoryName))
            {
                return false;
            }

            var modelToUpdate = allmodels.FirstOrDefault(x => x.Id == dtoModel.UseFullCategoryId);
            modelToUpdate = UseFullUpdatePostIputServiceMapper.Map(modelToUpdate, dtoModel);

            this.useFullCategories.Update(modelToUpdate);
            await this.useFullCategories.SaveAsync();

            return true;
        }
    }
}
