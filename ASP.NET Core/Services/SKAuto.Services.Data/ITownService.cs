namespace SKAuto.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;

    public interface ITownService
    {
        IQueryable<Town> GetAllTowns();

        Task CreateTownByNameAsync(string name);

        bool CheckIfExists(string name);
    }
}
