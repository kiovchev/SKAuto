namespace SKAutoNew.Services.Data.Contractcs
{
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecipientService
    {
        Task<bool> IfRecipientExistsAsync(string phoneNumber);

        // must be dto model
        Task<int> CreateRecipientAsync(string firstName,
                                       string lastName,
                                       string town,
                                       string address,
                                       string phone);

        Task<int> FindRecipientAsync(string number);

        Task<Recipient> GetRecipientByIdAsync(int recipientId);

        Task<List<RecipientIndexDto>> GetAllRecipientsAsync();

        Task<bool> DeleteRecipientAsync(RecipientDeleteDtoModel deleteDtoModel);

        Task<RecipientParamsDtoModel> GetRecipientParamsForUpdateAsync(RecipientUpdateOutputDtoModel dtoModel);

        Task<bool>  UpdateRecipientAsync(RecipientUpdateInputDtoModel dtoModel);
    }
}
