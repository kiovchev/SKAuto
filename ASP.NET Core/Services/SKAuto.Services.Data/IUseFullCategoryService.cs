namespace SKAuto.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;

    public interface IUseFullCategoryService
    {
        IQueryable<UseFullCategory> GetAllUseFullCategories();

        Task CreateUseFullCategoryByNameAsync(string name, string imageAddress);

        bool CheckIfExists(string name);
    }
}
