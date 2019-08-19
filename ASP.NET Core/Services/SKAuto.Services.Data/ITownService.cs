namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.TownViewModels;

    public interface ITownService
    {
        IQueryable<Town> GetAllTowns();

        TownWithCategoryNameViewModel GetTownsByCategoryName(string name);

        Task CreateTownByNameAsync(string name);

        bool CheckIfExists(string name);

        AllTownsViewModel GetTownNames();
    }
}
