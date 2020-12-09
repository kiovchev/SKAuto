namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Data.Models;
    using System.Threading.Tasks;

    public interface IManufactoryService
    {
        Task<Manufactory> GetManufactoryByNameAsync(string name);

        Task<Manufactory> CreateManufactoryAsync(string name);

        Task<Manufactory> GetManufactoryById(int? id);
    }
}
