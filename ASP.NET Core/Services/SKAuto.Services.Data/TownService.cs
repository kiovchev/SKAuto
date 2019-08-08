namespace SKAuto.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;

    public class TownService : ITownService
    {
        private readonly IRepository<Town> towns;

        public TownService(IRepository<Town> towns)
        {
            this.towns = towns;
        }

        public async Task CreateTownByName(string name)
        {
            Town town = new Town
            {
                Name = name,
            };

            await this.towns.AddAsync(town);
        }

        public IQueryable<Town> GetAllTowns()
        {
            var allTowns = this.towns.All();

            return allTowns;
        }
    }
}
