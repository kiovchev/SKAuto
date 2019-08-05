namespace SKAuto.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;

    public interface ICategoryService
    {
        IQueryable<Category> GetAllCategories();

        IQueryable<Category> GetCategoriesByNameAndYears(string modelName);

        Task CreateCategory(string name, string imageAddress);
    }
}
