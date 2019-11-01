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

        public Task CreateRecipientAndOrderAsync(PartByCategoryAndModelViewModel partModel, RecipientParamsViewModel paramsRecipient)
        {
            // need order
            throw new System.NotImplementedException();
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
