namespace SKAuto.Services.Data
{
    using System.Threading.Tasks;

    using SKAuto.Data.Models;

    public interface IManufactoryService
    {
        Task<Manufactory> GetManufactoryByNameAsync(string name);

        Task<Manufactory> CreateManufactoryAsync(string name);

        Task<Manufactory> GetManufactoryById(int? id);
    }
}
