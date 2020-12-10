namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUseFullCategoryService
    {
        Task<IList<UseFullCategory>> GetAlluseFullCategoriesAsync();

        Task<IList<UseFullCategoryWithImageViewDtoModel>> GetAllUseFullCategoriesWithParamsAsync();

        Task CreateUseFullCategoryByNameAsync(string name, string imageAddress);

        Task<bool> CheckIfExistsAsync(string name);
    }
}
