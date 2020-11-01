namespace SKAuto.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Common;
    using SKAuto.Common.DtoModels.RecipientDtos;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Services.Mapping.RecipientServiceMappers;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public class RecipientService : IRecipientService
    {
        private readonly IRepository<Recipient> recipients;
        private readonly IOrderService orderService;
        private readonly IPartService partService;

        public RecipientService(
                                IRepository<Recipient> recipients,
                                IOrderService orderService,
                                IPartService partService)
        {
            this.recipients = recipients;
            this.orderService = orderService;
            this.partService = partService;
        }

        public async Task<int> CreateRecipientAndOrderAsync(PartByCategoryAndModelViewModel partModel, RecipientParamsViewModel paramsRecipient)
        {
            // need order
            var recipient = new Recipient
            {
                FirstName = paramsRecipient.FirstName,
                LastName = paramsRecipient.LastName,
                Town = paramsRecipient.Town,
                Address = paramsRecipient.Address,
                Phone = paramsRecipient.Phone,
            };

            var currentOrder = await this.orderService.CreateOrderAsync(recipient, partModel);
            recipient.Orders.Add(currentOrder);

            await this.recipients.AddAsync(recipient);
            await this.recipients.SaveChangesAsync();

            return recipient.Id;
        }

        public async Task FindRecipientAndAddPartToAnOrder(int partid, int quantity, string number)
        {
            var recipient = await this.recipients.All()
                                                 .Include(x => x.Orders)
                                                 .ThenInclude(x => x.OrderStatus)
                                                 .FirstOrDefaultAsync(x => x.Phone == number);

            var order = recipient.Orders.FirstOrDefault(x => x.OrderStatus.Name == GlobalConstants.PendingStatus);
            if (order == null)
            {
            }
            else
            {
                // must go to part repository part in table minus quantity, after that go to order repo find create orderparts and add to orderparts part with quantity
            }
        }

        public async Task<RecipientFindOutPutDtoModel> GetPartParams(int partId)
        {
            var part = await this.partService.GetPartByIdAsync(partId);
            var dtoModel = RecipientServiceFindOutputMapper.Map(part);

            return dtoModel;
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
