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

    public class TownService : ITownService
    {
        private readonly IRepository<Town> towns;
        private readonly IRepository<TownUseFullCategory> townUseFullCategories;
        private readonly IUseFullCategoryService useFullCategoryService;
        //private readonly ICompanyService companyService;
        private readonly IRepository<Company> companyRepo;

        public TownService(
            IRepository<Town> towns,
            IRepository<TownUseFullCategory> townUseFullCategories,
            IUseFullCategoryService useFullCategoryService,
            //ICompanyService companyService,
            IRepository<Company> companyRepo)
        {
            this.towns = towns;
            this.townUseFullCategories = townUseFullCategories;
            this.useFullCategoryService = useFullCategoryService;
            //this.companyService = companyService;
            this.companyRepo = companyRepo;
        }

        public async Task<bool> CheckIfExistsAsync(string name)
        {
            bool checkTown = await this.towns.All().AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

            return checkTown;
        }

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

        public async Task<IList<Town>> GetAllTownsAsync()
        {
            var allTowns = await this.towns.All().ToListAsync();

            return allTowns;
        }

        public async Task<TownUpdateGetOutPutDto> GetTownById(TownUpdateGetInputDtoModel inputModel)
        {
            var neededTown = await this.towns.All().FirstOrDefaultAsync(x => x.Id == inputModel.TownId);
            var dtoModel = TownUpdateGetOutputServiceMapper.Map(neededTown);

            return dtoModel;
        }

        public async Task<AllTownsViewDtoModel> GetTownNamesAsync()
        {
            var allTowns = await this.towns.All().ToListAsync();
            var all = GetTownNamesMapper.Map(allTowns);

            return all;
        }

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

        public async Task<IList<TownIndexDtoModel>> GetTownsForIndexAsync()
        {
            var allTowns = await this.towns.All().ToListAsync();
            var townsDto = GetTownsForIndexMapper.Map(allTowns);

            return townsDto;
        }

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
