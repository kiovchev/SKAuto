namespace SKAuto.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public class RecipientService : IRecipientService
    {
        private readonly IRepository<Recipient> recipients;

        public RecipientService(IRepository<Recipient> recipients)
        {
            this.recipients = recipients;
        }

        public async Task<int> CreateRecipientAsync(RecipientParamsViewModel paramsRecipient)
        {
            var recipient = new Recipient
            {
                FirstName = paramsRecipient.FirstName,
                LastName = paramsRecipient.LastName,
                RecipientTown = paramsRecipient.Town,
                Address = paramsRecipient.Address,
                Phone = paramsRecipient.Phone,
            };

            await this.recipients.AddAsync(recipient);
            await this.recipients.SaveChangesAsync();
            var recipientId = recipient.Id;

            return recipientId;
        }

        public async Task<int> FindRecipientAsync(string number)
        {
            var recipient = await this.recipients.All().FirstOrDefaultAsync(x => x.Phone == number);
            var recipientId = 0;

            if (recipient != null)
            {
                recipientId = recipient.Id;
            }

            return recipientId;
        }

        public async Task<Recipient> GetRecipientByIdAsync(int recipientId)
        {
            var recipient = await this.recipients.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == recipientId);

            return recipient;
        }

        public async Task<bool> IfRecipientExistsAsync(string phoneNumber)
        {
            var searchedPhone = await this.recipients.All().Select(x => x.Phone).FirstOrDefaultAsync(x => x == phoneNumber);

            if (searchedPhone == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
