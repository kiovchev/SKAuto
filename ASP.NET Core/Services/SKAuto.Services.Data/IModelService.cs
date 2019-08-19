namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.ModelViewModels;

    public interface IModelService
    {
        Task<IList<ModelsWithImage>> GetAllModelsAsync(ModelKindInputModel kindInputModel);

        Task CreateModel(ModelInputViewModel modelInputViewModel);

        Task<bool> IfModelExists(string brandName, string modelName, int startYear, int endYear);
    }
}
