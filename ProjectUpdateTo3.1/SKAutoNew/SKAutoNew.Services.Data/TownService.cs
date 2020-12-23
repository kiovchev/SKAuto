namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Mappers.TownServiceMappers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// business logic for Town
    /// </summary>
    public class TownService : ITownService
    {
        private readonly IRepository<Town> towns;
        private readonly IRepository<TownUseFullCategory> townUseFullCategories;
        private readonly IUseFullCategoryService useFullCategoryService;
        private readonly IRepository<Company> companyRepo;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="towns"></param>
        /// <param name="townUseFullCategories"></param>
        /// <param name="useFullCategoryService"></param>
        /// <param name="companyRepo"></param>
        public TownService(
            IRepository<Town> towns,
            IRepository<TownUseFullCategory> townUseFullCategories,
            IUseFullCategoryService useFullCategoryService,
            IRepository<Company> companyRepo)
        {
            this.towns = towns;
            this.townUseFullCategories = townUseFullCategories;
            this.useFullCategoryService = useFullCategoryService;
            this.companyRepo = companyRepo;
        }

        /// <summary>
        /// check in database if there is same recipient
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfExistsAsync(string name)
        {
            bool checkTown = await this.towns.All().AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

            return checkTown;
        }

        /// <summary>
        /// create new town, create townusefullcategories and add them all to database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task CreateTownByNameAsync(string name)
        {
            var allUseFullCategories = await this.useFullCategoryService.GetAlluseFullCategoriesAsync();

            Town town = new Town
            {
                Name = name,
            };

            foreach (var useFullCategory in allUseFullCategories)
            {
                TownUseFullCategory townUseFullCategory = new TownUseFullCategory()
                {
                    UseFullCategory = useFullCategory,
                    Town = town,
                };

                await this.townUseFullCategories.InsertAsync(townUseFullCategory);
            }

            await this.towns.InsertAsync(town);
            await this.towns.SaveAsync();
        }

        /// <summary>
        /// delete town from database
        /// </summary>
        /// <param name="deleteDtoModel"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTownAsync(TownDeleteDtoModel deleteDtoModel)
        {
            var neededTown = await this.towns.All()
                                             .Include(x => x.UseFullCategories)
                                             .Include(x => x.Companies)
                                             .FirstOrDefaultAsync(x => x.Id == deleteDtoModel.TownId);

            if (neededTown == null)
            {
                return false;
            }

            foreach (var item in neededTown.Companies.ToList())
            {
                this.companyRepo.Delete(item);
            }

            foreach (var item in neededTown.UseFullCategories.ToList())
            {
                this.townUseFullCategories.Delete(item);
            }

            this.towns.Delete(neededTown);
            await this.towns.SaveAsync();

            return true;
        }

        /// <summary>
        /// get all towns from database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Town>> GetAllTownsAsync()
        {
            var allTowns = await this.towns.All().ToListAsync();

            return allTowns;
        }

        /// <summary>
        /// find town by id and get it from database
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<TownUpdateGetOutPutDto> GetTownById(TownUpdateGetInputDtoModel inputModel)
        {
            var neededTown = await this.towns.All().FirstOrDefaultAsync(x => x.Id == inputModel.TownId);
            var dtoModel = TownUpdateGetOutputServiceMapper.Map(neededTown);

            return dtoModel;
        }

        /// <summary>
        /// get all towns from database and return model that contains property for town names
        /// </summary>
        /// <returns></returns>
        public async Task<AllTownsViewDtoModel> GetTownNamesAsync()
        {
            var allTowns = await this.towns.All().ToListAsync();
            var all = GetTownNamesMapper.Map(allTowns);

            return all;
        }

        /// <summary>
        /// get all towns by usefullcategory name from database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<TownWithCategoryNameViewDtoModel> GetTownsByCategoryNameAsync(string name)
        {
            var allTowns = await this.towns.All()
                                           .Include(x => x.UseFullCategories)
                                           .SelectMany(x => x.UseFullCategories
                                           .Where(y => y.UseFullCategory.Name == name))
                                           .Select(x => x.Town.Name)
                                           .OrderBy(x => x)
                                           .ToListAsync();
            var viewModel = TownWithCategoryNameMapper.Map(name, allTowns);

            return viewModel;
        }

        /// <summary>
        /// get all towns from database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<TownIndexDtoModel>> GetTownsForIndexAsync()
        {
            var allTowns = await this.towns.All().ToListAsync();
            var townsDto = GetTownsForIndexMapper.Map(allTowns);

            return townsDto;
        }

        /// <summary>
        /// change town and update it in database
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTownAsync(TownUpdatePostInputDtoModel inputModel)
        {
            var alltowns = await this.towns.All().ToListAsync();

            if (alltowns.Any(x => x.Name == inputModel.TownName))
            {
                return false;
            }

            var neededTown = alltowns.FirstOrDefault(x => x.Id == inputModel.TownId);

            if (neededTown == null)
            {
                return false;
            }

            neededTown.Name = inputModel.TownName;
            this.towns.Update(neededTown);
            await this.towns.SaveAsync();

            return true;
        }
    }
}
