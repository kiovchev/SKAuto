namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModelService
    {
        Task<IList<ModelWithImageDtoModel>> GetAllModelsByBrandNameAsync(string kindInputModel);

        Task CreateModel(ModelCreateDtoModel modelToCreate);

        Task<bool> IfModelExistsAsync(string brandName, string modelName, int startYear, int endYear);

        Task<bool> IsSameAsync(string brandName, string modelName, int startYear, int endYear, string imageAddress);

        Task<IList<ModelWithBrandNameDtoModel>> GetAllModels();

        Task DeleteModelAsync(int id);

        Task<bool> HavePartsAsync(int id);

        Task<ModelUpdateOutputDtoModel> GetModelByIdAsync(int id);

        Task<Model> GetModelByNameStartAndEndYearsAsync(string name, int startYear, int endYear);

        Task UpdateModelAsync(ModelUpdateInputDtoModel model);
    }
}
