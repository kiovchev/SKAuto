namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITownService
    {
        Task<IList<Town>> GetAllTownsAsync();

        Task<TownWithCategoryNameViewDtoModel> GetTownsByCategoryNameAsync(string name);

        Task CreateTownByNameAsync(string name);

        Task<bool> CheckIfExistsAsync(string name);

        Task<AllTownsViewDtoModel> GetTownNamesAsync();

        Task<IList<TownIndexDtoModel>> GetTownsForIndexAsync();
    }
}
