namespace SKAuto.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.ModelViewModels;

    public interface IModelService
    {
        IQueryable<Model> GetAllModelsByBrandId(int id);

        IQueryable<Model> GetAllModels(int id);

        Task CreateModel(ModelInputViewModel modelInputViewModel, int brandId);
    }
}
