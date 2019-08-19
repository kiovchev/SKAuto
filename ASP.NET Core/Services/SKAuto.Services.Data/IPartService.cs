namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public interface IPartService
    {
        Task CreatePartAsync(PartCreateInputModel partModel);

        Task<List<PartByCategoryAndModelViewModel>> GetPartsByModelAndCategoryAsync(string modelName, string categoryName);

        PartCreateViewModel GetPartCreateParams();
    }
}
