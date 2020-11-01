namespace SKAuto.Services.Data
{
    using System.Threading.Tasks;

    using SKAuto.Common.DtoModels.RecipientDtos;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public interface IRecipientService
    {
        Task<bool> IfRecipientExistsAsync(string phoneNumber);

        Task<int> CreateRecipientAndOrderAsync(
            PartByCategoryAndModelViewModel partModel,
            RecipientParamsViewModel paramsRecipient);

        Task<RecipientFindOutPutDtoModel> GetPartParams(int partId);

        Task FindRecipientAndAddPartToAnOrder(int partid, int quantity, string number);
    }
}
