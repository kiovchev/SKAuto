namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Common.DtoModels.ModelDto;
    using SKAuto.Web.ViewModels.ViewModels.ModelViewModels;

    public interface IModelService
    {
        Task<IList<ModelsWithImage>> GetAllModelsByBrandNameAsync(ModelKindInputModel kindInputModel);

        Task CreateModel(ModelInputViewModel modelInputViewModel);

        Task<bool> IfModelExistsAsync(string brandName, string modelName, int startYear, int endYear);

        Task<bool> IsSameAsync(string brandName, string modelName, int startYear, int endYear, string imageAddress);

        Task<int> GetCountOfModelsByBrandIdAsync(int id);

        Task<IList<ModelWithBrandNameDtoModel>> GetAllModels();

        Task DeleteModelAsync(int id);

        Task<bool> HavePartsAsync(int id);

        Task<ModelUpdateOutputDtoModel> GetModelByIdAsync(int id);

        Task UpdateModelAsync(ModelUpdateInputDtoModel model);
    }
}
