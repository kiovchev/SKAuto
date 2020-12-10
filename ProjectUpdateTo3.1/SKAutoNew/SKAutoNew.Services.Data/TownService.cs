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
        private readonly IRepository<UseFullCategory> useFullCategories;
        private readonly IRepository<TownUseFullCategory> townUseFullCategories;

        public TownService(
            IRepository<Town> towns,
            IRepository<UseFullCategory> useFullCategories,
            IRepository<TownUseFullCategory> townUseFullCategories)
        {
            this.towns = towns;
            this.useFullCategories = useFullCategories;
            this.townUseFullCategories = townUseFullCategories;
        }

        public async Task<bool> CheckIfExistsAsync(string name)
        {
            bool checkTown = await this.towns.All().AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

            return checkTown;
        }

        public async Task CreateTownByNameAsync(string name)
        {
            var allUseFullCategories = this.useFullCategories.All();

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

        public async Task<IList<Town>> GetAllTownsAsync()
        {
            var allTowns = await this.towns.AllAsNoTracking().ToListAsync();

            return allTowns;
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
    }
}
