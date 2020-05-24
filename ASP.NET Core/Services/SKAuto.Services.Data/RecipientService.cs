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
        private readonly IOrderService orderService;

        public RecipientService(IRepository<Recipient> recipients, IOrderService orderService)
        {
            this.recipients = recipients;
            this.orderService = orderService;
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
