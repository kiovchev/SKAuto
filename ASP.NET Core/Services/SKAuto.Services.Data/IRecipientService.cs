namespace SKAuto.Services.Data
{
    using System.Threading.Tasks;

    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public interface IRecipientService
    {
        Task<bool> IfRecipientExistsAsync(string phoneNumber);

        Task<int> CreateRecipientAndOrderAsync(
            PartByCategoryAndModelViewModel partModel,
            RecipientParamsViewModel paramsRecipient);
    }
}
