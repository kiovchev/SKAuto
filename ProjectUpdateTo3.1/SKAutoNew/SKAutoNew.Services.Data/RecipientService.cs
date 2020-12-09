namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Mappers.RecipientServiceMappers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RecipientService : IRecipientService
    {
        private readonly IRepository<Recipient> recipients;

        public RecipientService(IRepository<Recipient> recipients)
        {
            this.recipients = recipients;
        }

        public async Task<int> CreateRecipientAsync(string firstName, 
                                                    string lastName,
                                                    string town,
                                                    string address,
                                                    string phone)
        {
            var recipient = new Recipient
            {
                FirstName = firstName,
                LastName = lastName,
                RecipientTown = town,
                Address = address,
                Phone = phone,
            };

            await this.recipients.InsertAsync(recipient);
            await this.recipients.SaveAsync();
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

        public async Task<List<RecipientIndexDto>> GetAllRecipientsAsync()
        {
            var recpientsAll = await this.recipients.AllAsNoTracking().ToListAsync();
            var recipientsForIndex = RecipientServiceIndexMapper.Map(recpientsAll);

            return recipientsForIndex;
        }

        public async Task<Recipient> GetRecipientByIdAsync(int recipientId)
        {
            var recipient = await this.recipients.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == recipientId);

            return recipient;
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
    }
}
