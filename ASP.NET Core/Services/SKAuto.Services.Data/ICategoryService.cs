namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Web.ViewModels.ViewModels.CategoryViewModels;

    public interface ICategoryService
    {
        IList<CategoryWithImageViewModel> GetAllCategoriesForViewModel();

        IList<CategoryWithModelViewModel> GetCategoriesByNameAndYears(string modelName);

        Task CreateCategory(string name, string imageAddress);

        bool IfCategoryExists(string name);
    }
}
