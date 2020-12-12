namespace SKAutoNew.Services.Mappers.OrderServiceMappers
{
    using SKAutoNew.Common.DtoModels.OrderDtos;
    using SKAutoNew.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class OrderUpdateParamsServiceMapper
    {
        public static UpdateOutPutOrderDtoModel Map(Order order, IList<string> allOrderStatusesNames)
        {
            var index = allOrderStatusesNames.IndexOf(order.OrderStatus.Name);
            allOrderStatusesNames.RemoveAt(index);

            var model = new UpdateOutPutOrderDtoModel
            {
                Id = order.Id,
                IssuedOn = order.IssuedOn,
                OrderStatusName = order.OrderStatus.Name,
                RecipientName = $"{order.Recipient.FirstName} {order.Recipient.LastName}",
                OrderTotalPrice = Math.Round(order.Items.Select(y => y.Part.CustomerPrice * y.OrderedQuantity).Sum(), 2),
                AllOrderStatusesNames = allOrderStatusesNames
            };

            return model;
        }
    }
}
