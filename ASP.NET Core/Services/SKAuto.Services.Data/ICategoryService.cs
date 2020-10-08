namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Web.ViewModels.ViewModels.CategoryViewModels;

    public interface ICategoryService
    {
        Task<IList<CategoryIndexDtoModel>> GetAllCategories();

        Task<IList<CategoruAllDtoModel>> GetAllCategoriesForViewModel();

        Task<IList<CategoryWithModelViewModel>> GetCategoriesByNameAndYears(string modelName);

        Task CreateCategory(string name, string imageAddress);

        Task<bool> IfCategoryExists(string name);
    }
}
