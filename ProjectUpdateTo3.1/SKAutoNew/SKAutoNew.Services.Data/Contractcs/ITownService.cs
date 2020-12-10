namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using System.Threading.Tasks;

    public interface ITownService
    {
        Task<TownWithCategoryNameViewDtoModel> GetTownsByCategoryNameAsync(string name);

        Task CreateTownByNameAsync(string name);

        Task<bool> CheckIfExistsAsync(string name);

        Task<AllTownsViewDtoModel> GetTownNamesAsync();
    }
}
