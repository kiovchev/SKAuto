namespace SKAuto.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;

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

            await this.manufactories.AddAsync(manufactory);
            await this.manufactories.SaveChangesAsync();

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
