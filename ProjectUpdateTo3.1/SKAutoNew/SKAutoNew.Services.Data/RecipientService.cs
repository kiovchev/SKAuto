namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.HandMappers.RecipientMappers;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Mappers.RecipientServiceMappers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RecipientService : IRecipientService
    {
        private readonly IRepository<Recipient> recipients;
        private readonly IOrderService orderService;

        public RecipientService(IRepository<Recipient> recipients, IOrderService orderService)
        {
            this.recipients = recipients;
            this.orderService = orderService;
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

        public async Task<bool> DeleteRecipientAsync(RecipientDeleteDtoModel deleteDtoModel)
        {
            var modelToDelete = await this.recipients.All().FirstAsync(x => x.Id == deleteDtoModel.RecipientId);

            if (modelToDelete == null)
            {
                return false;
            }

            await this.orderService.DeleteAllOrdersByRecipientIdAsync(modelToDelete.Id);

            this.recipients.Delete(modelToDelete);
            await this.recipients.SaveAsync();

            return true;
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

        public async Task<RecipientParamsDtoModel> GetRecipientParamsForUpdateAsync(RecipientUpdateOutputDtoModel dtoModel)
        {
            var modelForUpdate = await this.recipients.All().FirstOrDefaultAsync(x => x.Id == dtoModel.RecipientId);
            var paramsModel = RecipientParamsDtoMapper.Map(modelForUpdate);

            return paramsModel;
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

        public async Task<bool> UpdateRecipientAsync(RecipientUpdateInputDtoModel dtoModel)
        {
            var recipientForUpdate = await this.recipients.All().FirstOrDefaultAsync(x => x.Id == dtoModel.RecipientId);

            if (recipientForUpdate.Id == dtoModel.RecipientId 
                && recipientForUpdate.FirstName == dtoModel.FirstName
                && recipientForUpdate.LastName == dtoModel.LastName
                && recipientForUpdate.RecipientTown == dtoModel.RecipientTown
                && recipientForUpdate.Address == dtoModel.Address
                && recipientForUpdate.Phone == dtoModel.Phone)
            {
                return false;
            }

            recipientForUpdate = RecipientUpdateServiceMapper.Map(recipientForUpdate, dtoModel);

            this.recipients.Update(recipientForUpdate);
            await this.recipients.SaveAsync();

            return true;
        }
    }
}
