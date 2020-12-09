namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using System.Threading.Tasks;

    public class ManufactoryService : IManufactoryService
    {
        private readonly IRepository<Manufactory> manufactories;

        public ManufactoryService(IRepository<Manufactory> manufactories)
        {
            this.manufactories = manufactories;
        }

        public async Task<Manufactory> CreateManufactoryAsync(string name)
        {
            var manufactory = new Manufactory
            {
                Name = name,
            };

            await this.manufactories.InsertAsync(manufactory);
            await this.manufactories.SaveAsync();

            return manufactory;
        }

        public async Task<Manufactory> GetManufactoryById(int? id)
        {
            var manufactory = await this.manufactories.All().FirstOrDefaultAsync(x => x.Id == id);

            return manufactory;
        }

        public async Task<Manufactory> GetManufactoryByNameAsync(string name)
        {
            var manufactory = await this.manufactories.All().FirstOrDefaultAsync(x => x.Name == name);

            return manufactory;
        }
    }
}
