namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;

    public interface ITownService
    {
        IQueryable<Town> GetAllTowns();

        IList<string> GetTownsByCategoryName(string name);

        Task CreateTownByNameAsync(string name);

        bool CheckIfExists(string name);
    }
}
