namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Common.DtoModels.CategoryDtos;

    public interface ICategoryService
    {
        Task<CategoryUpdateOutputDtoModel> GetCategoryByIdAsync(int categoryId);

        Task<IList<CategoryIndexDtoModel>> GetAllCategoriesAsync();

        Task<IList<CategoryAllDtoModel>> GetAllCategoriesForViewModelAsync();

        Task<IList<GetCategoriesByNameAndYearsDtoModel>> GetCategoriesByNameAndYears(string modelName);

        Task CreateCategoryAsync(CategoryCreateDtoModel categoryCreateDto);

        Task<bool> IfCategoryExists(string name);

        Task<bool> IsSameCategoryAsync(string name, string imageAddress);

        Task UpdateCategoryAsync(CategoryUpdateInputDtoModel categoryDto);

        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
