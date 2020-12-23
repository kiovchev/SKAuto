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

    /// <summary>
    /// business logic for Recipient
    /// </summary>
    public class RecipientService : IRecipientService
    {
        private readonly IRepository<Recipient> recipients;
        private readonly IOrderService orderService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="orderService"></param>
        public RecipientService(IRepository<Recipient> recipients, IOrderService orderService)
        {
            this.recipients = recipients;
            this.orderService = orderService;
        }

        /// <summary>
        /// create new recipient and add it to database
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="town"></param>
        /// <param name="address"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
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

        /// <summary>
        /// find recipient by id and delete it from database
        /// </summary>
        /// <param name="deleteDtoModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// find recipient by phone number and return it id
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
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

        /// <summary>
        /// get all recipients from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<RecipientIndexDto>> GetAllRecipientsAsync()
        {
            var recpientsAll = await this.recipients.AllAsNoTracking().ToListAsync();
            var recipientsForIndex = RecipientServiceIndexMapper.Map(recpientsAll);

            return recipientsForIndex;
        }

        /// <summary>
        /// find recipient by id and get it from database
        /// </summary>
        /// <param name="recipientId"></param>
        /// <returns></returns>
        public async Task<Recipient> GetRecipientByIdAsync(int recipientId)
        {
            var recipient = await this.recipients.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == recipientId);

            return recipient;
        }

        /// <summary>
        /// get params for recipient, needed for an update view
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns></returns>
        public async Task<RecipientParamsDtoModel> GetRecipientParamsForUpdateAsync(RecipientUpdateOutputDtoModel dtoModel)
        {
            var modelForUpdate = await this.recipients.All().FirstOrDefaultAsync(x => x.Id == dtoModel.RecipientId);
            var paramsModel = RecipientParamsDtoMapper.Map(modelForUpdate);

            return paramsModel;
        }

        /// <summary>
        /// check if there is a recipient with same phone number in database
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
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

        /// <summary>
        /// update recipient in datbase
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns></returns>
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
