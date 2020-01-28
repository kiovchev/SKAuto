namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.OrderViewModels;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;
    using SKAuto.Web.ViewModels.ViewModels.RecipientViewModels;

    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orders;
        private readonly IRepository<Recipient> recipients;

        public OrderService(IRepository<Order> orders, IRepository<Recipient> recipients)
        {
            this.orders = orders;
            this.recipients = recipients;
        }

        public async Task<List<OrdersByIdViewModel>> TakeOrdersByRecipientIdAsync(RecipientFindInputModel findInputModel)
        {
            int recipientId = await this.recipients.All().Where(x => x.FirstName == findInputModel.FirstName
            && x.LastName == findInputModel.LastName
            && x.Phone == findInputModel.Phone).Select(x => x.Id).FirstOrDefaultAsync();

            var orders = this.orders.All()
                .Include(x => x.OrderParts)
                .Where(x => x.RecipientId == recipientId)
                .OrderBy(x => x.IssuedOn)
                .Select(x => new OrdersByIdViewModel
                {
                    IssuedOn = x.IssuedOn,
                    StatusName = x.OrderStatus.Name,
                    OrderParts = x.OrderParts.Select(y => new PartCreateInputModel
                    {
                        PartName = y.Part.Name,
                        CategoryName = y.Part.Category.Name,
                        ModelName = y.Part.Model.Name,
                        Price = y.Part.CustomerPrice,
                        ManufactoryName = y.Part.Manufactory.Name,
                        Quantity = y.Part.Quantity,
                    })
                    .OrderBy(z => z.PartName)
                    .ToList(),
                })
                .ToList();

            return orders;
        }
    }
}
