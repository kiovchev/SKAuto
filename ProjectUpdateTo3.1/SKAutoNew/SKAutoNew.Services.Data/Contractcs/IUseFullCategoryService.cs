namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUseFullCategoryService
    {
        Task<List<UseFullCategoryWithImageViewDtoModel>> GetAllUseFullCategoriesWithParamsAsync();

        Task CreateUseFullCategoryByNameAsync(string name, string imageAddress);

        Task<bool> CheckIfExistsAsync(string name);
    }
}
