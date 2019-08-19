namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.UseFullCategoryViewModels;

    public interface IUseFullCategoryService
    {
        IQueryable<UseFullCategory> GetAllUseFullCategories();

        List<UseFullCategoryWithImageViewModel> GetAllUseFullCategoriesWithParams();

        Task CreateUseFullCategoryByNameAsync(string name, string imageAddress);

        bool CheckIfExists(string name);
    }
}
