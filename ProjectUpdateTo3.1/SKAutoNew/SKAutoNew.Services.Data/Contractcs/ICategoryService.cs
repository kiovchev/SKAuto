namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<CategoryUpdateOutputDtoModel> GetCategoryByIdAsync(int categoryId);

        Task<IList<CategoryIndexDtoModel>> GetAllCategoriesAsync();

        Task<Category> GetCategoryByNameAsync(string categoryName);

        Task<IList<string>> GetAllCategoryNamesAsync();

        Task<IList<Category>> GetAllCategoriesForModelAsync();

        Task<IList<CategoryAllDtoModel>> GetAllCategoriesForViewModelAsync();

        Task<IList<GetCategoriesByNameAndYearsDtoModel>> GetCategoriesByNameAndYears(string modelName);

        Task CreateCategoryAsync(CategoryCreateDtoModel categoryCreateDto);

        Task<bool> IfCategoryExists(string name);

        Task<bool> IsSameCategoryAsync(string name, string imageAddress);

        Task UpdateCategoryAsync(CategoryUpdateInputDtoModel categoryDto);

        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
