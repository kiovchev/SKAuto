namespace SKAuto.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public class RecipientService : IRecipientService
    {
        private readonly IRepository<Recipient> recipients;

        public RecipientService(IRepository<Recipient> recipients)
        {
            this.recipients = recipients;
        }

        public async Task<int> CreateRecipientAndOrderAsync(RecipientParamsViewModel paramsRecipient)
        {
            Recipient recipient = new Recipient()
            {
                FirstName = paramsRecipient.FirstName,
                LastName = paramsRecipient.LastName,
                Address = paramsRecipient.Address,
                RecipientTown = paramsRecipient.Town,
                Phone = paramsRecipient.Phone,
            };

            await this.recipients.AddAsync(recipient);
            await this.recipients.SaveChangesAsync();
            int recipientId = this.recipients.All().Where(x => x.Phone == paramsRecipient.Phone
           && x.FirstName == paramsRecipient.FirstName && x.LastName == paramsRecipient.LastName)
                .Select(x => x.Id).FirstOrDefault();

            return recipientId;
        }

        public async Task<bool> IfRecipientExistsAsync(string phoneNumber)
        {
            var searchedPhone = await this.recipients.All()
                .Select(x => x.Phone)
                .FirstOrDefaultAsync(x => x == phoneNumber);

            if (searchedPhone == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<RecipientOrderIpnutModel> TakeRecipientIdAsync(PartByCategoryAndModelViewModel partModel, RecipientFindInputModel recipientFindModel)
        {
            int recipientId = await this.recipients.All().Where(x => x.Phone == recipientFindModel.Phone
           && x.FirstName == recipientFindModel.FirstName && x.LastName == recipientFindModel.LastName)
                .Select(x => x.Id).FirstOrDefaultAsync();

            RecipientOrderIpnutModel recipientOrderIpnutModel = new RecipientOrderIpnutModel()
            {
                PartModel = partModel,
                RecipientId = recipientId,
            };

            return recipientOrderIpnutModel;
        }
    }
}
