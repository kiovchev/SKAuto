namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using System.Threading.Tasks;

    /// <summary>
    /// business logic for Manufactory
    /// </summary>
    public class ManufactoryService : IManufactoryService
    {
        private readonly IRepository<Manufactory> manufactories;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="manufactories"></param>
        public ManufactoryService(IRepository<Manufactory> manufactories)
        {
            this.manufactories = manufactories;
        }

        /// <summary>
        /// create new manufactory and add it in database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// get manufactory from database using id to find it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Manufactory> GetManufactoryById(int? id)
        {
            var manufactory = await this.manufactories.All().FirstOrDefaultAsync(x => x.Id == id);

            return manufactory;
        }

        /// <summary>
        /// find manufactory in database using name and get it 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Manufactory> GetManufactoryByNameAsync(string name)
        {
            var manufactory = await this.manufactories.All().FirstOrDefaultAsync(x => x.Name == name);

            return manufactory;
        }
    }
}
