﻿namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Common.DtoModels.RecipientDtos;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public interface IRecipientService
    {
        Task<bool> IfRecipientExistsAsync(string phoneNumber);

        Task<int> CreateRecipientAsync(RecipientParamsViewModel paramsRecipient);

        Task<int> FindRecipientAsync(string number);

        Task<Recipient> GetRecipientByIdAsync(int recipientId);

        Task<List<RecipientIndexDto>> GetAllRecipientsAsync();
    }
}
