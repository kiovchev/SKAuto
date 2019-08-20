namespace SKAuto.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.TownViewModels;

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

        public bool CheckIfExists(string name)
        {
            bool checkTown = this.towns.All().Any(x => x.Name.ToUpper() == name.ToUpper());

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

                await this.townUseFullCategories.AddAsync(townUseFullCategory);
            }

            await this.towns.AddAsync(town);
            await this.towns.SaveChangesAsync();
        }

        public IQueryable<Town> GetAllTowns()
        {
            var allTowns = this.towns.All();

            return allTowns;
        }

        public AllTownsViewModel GetTownNames()
        {
            var allTowns = this.towns.All();

            AllTownsViewModel all = new AllTownsViewModel();

            foreach (var town in allTowns)
            {
                all.TownsNames.Add(town.Name);
            }

            return all;
        }

        public TownWithCategoryNameViewModel GetTownsByCategoryName(string name)
        {
            var allTowns = this.towns.All().Include(x => x.UseFullCategories)
                                           .SelectMany(x => x.UseFullCategories
                                           .Where(y => y.UseFullCategory.Name == name))
                                           .Select(x => x.Town.Name)
                                           .OrderBy(x => x)
                                           .ToList();

            TownWithCategoryNameViewModel viewModel = new TownWithCategoryNameViewModel()
            {
                CategoryName = name,
                TownNames = allTowns,
            };

            return viewModel;
        }
    }
}
